#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>
#include <iomanip>
#include <filesystem>
#include <cmath>

using namespace std;

typedef vector<vector<double>> Matrix;

Matrix readCSV(const string& filename) {
    Matrix matrix;
    ifstream file(filename);

    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << endl;
        exit(EXIT_FAILURE);
    }

    string line;
    size_t columnCount = 0;
    while (getline(file, line)) {
        if (line.empty()) continue; 
        stringstream ss(line);
        string value;
        vector<double> row;

        while (getline(ss, value, ',')) {
            try {
                row.push_back(stod(value));
            }
            catch (const invalid_argument& e) {
                cerr << "Error: Invalid value in CSV file: " << value << endl;
                exit(EXIT_FAILURE);
            }
        }

        if (columnCount == 0) {
            columnCount = row.size();
        }
        else if (row.size() != columnCount) {
            cerr << "Error: Inconsistent column count in input file." << endl;
            exit(EXIT_FAILURE);
        }

        matrix.push_back(row);
    }

    if (matrix.empty()) {
        cerr << "Error: The input file is empty or incorrectly formatted." << endl;
        exit(EXIT_FAILURE);
    }

    return matrix;
}

void writeResultToFile(const string& filename, const Matrix& rrefMatrix, int rankA, int rankAb, bool consistent, const vector<string>& solutions) {
    ofstream file(filename);

    for (const auto& row : rrefMatrix) {
        for (size_t j = 0; j < row.size(); ++j) {
            file << static_cast<int>(round(row[j]));
            if (j < row.size() - 1) file << "\t";
        }
        file << "\n";
    }

    file << "Rank(A)=" << rankA << "\n";
    file << "Rank(Ab)=" << rankAb << "\n";
    file << (consistent ? "Consistent" : "Not consistent") << "\n";

    if (consistent) {
        for (const string& solution : solutions) {
            file << solution << "\n";
        }
    }
}

Matrix calculateRREF(Matrix matrix) {
    size_t rows = matrix.size();
    size_t cols = matrix[0].size();
    size_t lead = 0;

    for (size_t r = 0; r < rows; ++r) {
        if (lead >= cols) break;

        size_t i = r;
        while (i < rows && fabs(matrix[i][lead]) < 1e-9) {
            ++i;
        }

        if (i == rows) {
            ++lead;
            if (lead == cols) break;
            continue;
        }

        swap(matrix[i], matrix[r]);
        double lv = matrix[r][lead];
        for (size_t j = 0; j < cols; ++j) {
            matrix[r][j] /= lv;
        }

        for (size_t i = 0; i < rows; ++i) {
            if (i != r) {
                lv = matrix[i][lead];
                for (size_t j = 0; j < cols; ++j) {
                    matrix[i][j] -= lv * matrix[r][j];
                }
            }
        }
        ++lead;
    }
    return matrix;
}

int calculateRank(const Matrix& matrix) {
    int rank = 0;
    for (const auto& row : matrix) {
        bool nonZeroRow = false;
        for (double val : row) {
            if (fabs(val) > 1e-9) {
                nonZeroRow = true;
                break;
            }
        }
        if (nonZeroRow) ++rank;
    }
    return rank;
}

vector<string> generateSolutions(const Matrix& rrefMatrix, int numVariables) {
    vector<string> solutions(numVariables, "");
    vector<bool> isBasicVariable(numVariables, false);
    int lead = 0;

    for (size_t r = 0; r < rrefMatrix.size(); ++r) {
        while (lead < numVariables && fabs(rrefMatrix[r][lead]) < 1e-9) {
            ++lead;
        }
        if (lead < numVariables) {
            isBasicVariable[lead] = true;
            ++lead;
        }
    }

    vector<string> freeVariables;
    for (int i = 0; i < numVariables; ++i) {
        if (!isBasicVariable[i]) {
            freeVariables.push_back("x" + to_string(i + 1));
        }
    }

    for (int i = 0; i < numVariables; ++i) {
        if (isBasicVariable[i]) {
            stringstream ss;
            ss << "x" << (i + 1) << "=" << static_cast<int>(round(rrefMatrix[i].back()));
            for (int j = 0; j < numVariables; ++j) {
                if (!isBasicVariable[j] && fabs(rrefMatrix[i][j]) > 1e-9) {
                    ss << (rrefMatrix[i][j] < 0 ? "+" : "-")
                        << fabs(static_cast<int>(round(rrefMatrix[i][j]))) << "x" << (j + 1);
                }
            }
            solutions[i] = ss.str();
        }
        else {
            solutions[i] = "x" + to_string(i + 1) + "=free";
        }
    }

    return solutions;
}

int main() {
    string inputFile = "case3_input.csv";
    string outputFile = "output.txt";

    Matrix matrix = readCSV(inputFile);

    Matrix rref = calculateRREF(matrix);

    size_t cols = matrix[0].size();
    Matrix A(matrix);
    for (auto& row : A) row.pop_back();
    A = calculateRREF(A);
    int rankA = calculateRank(A);
    int rankAb = calculateRank(rref);

    bool consistent = (rankA == rankAb);

    vector<string> solutions;
    if (consistent) {
        solutions = generateSolutions(rref, cols - 1);
    }

    writeResultToFile(outputFile, rref, rankA, rankAb, consistent, solutions);

    cout << "RREF has been calculated and saved to " << outputFile << endl;

    return 0;
}
