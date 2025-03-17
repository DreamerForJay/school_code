using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week3_hw_test
{
    public partial class Form1 : Form
    {
        Button[,] board = new Button[3, 3];
        int[,] state = new int[3, 3];
        bool start = false;  // true: 開始可以點選
        bool computer = true; // true: 換電腦下 false: 換玩家下
        int count; // 計算已經下了幾次，9次結束

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board[0, 0] = button1;
            board[0, 1] = button2;
            board[0, 2] = button3;
            board[1, 0] = button4;
            board[1, 1] = button5;
            board[1, 2] = button6;
            board[2, 0] = button7;
            board[2, 1] = button8;
            board[2, 2] = button9;
        }
        void showBoard()
        {
            int i, j;
            for (i = 0; i < 3; i++)
                for (j = 0; j < 3; j++)
                {
                    if (state[i, j] == 0) board[i, j].Text = "";
                    if (state[i, j] == 1) board[i, j].Text = "O";
                    if (state[i, j] == 10) board[i, j].Text = "X";
                }
            if (computer)
                textBox1.Text = "電腦";
            else
                textBox1.Text = "玩家";

            // **檢查對角線勝利 (電腦勝利)**
            if (state[0, 0] + state[1, 1] + state[2, 2] == 3 || state[0, 2] + state[1, 1] + state[2, 0] == 3)
            {
                MessageBox.Show("電腦獲勝，遊戲結束!", "遊戲結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                start = false;
                return;
            }

            // **檢查橫排與直排勝利 (電腦勝利)**
            for (i = 0; i < 3; i++)
            {
                if (state[i, 0] + state[i, 1] + state[i, 2] == 3 || state[0, i] + state[1, i] + state[2, i] == 3)
                {
                    MessageBox.Show("電腦獲勝，遊戲結束!", "遊戲結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    start = false;
                    return;
                }
            }

            // **檢查對角線勝利 (玩家獲勝)**
            if (state[0, 0] + state[1, 1] + state[2, 2] == 30 || state[0, 2] + state[1, 1] + state[2, 0] == 30)
            {
                MessageBox.Show("恭喜玩家獲勝，遊戲結束!", "遊戲結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                start = false;
                return;
            }

            // **檢查橫排與直排勝利 (玩家獲勝)**
            for (i = 0; i < 3; i++)
            {
                if (state[i, 0] + state[i, 1] + state[i, 2] == 30 || state[0, i] + state[1, i] + state[2, i] == 30)
                {
                    MessageBox.Show("恭喜玩家獲勝，遊戲結束!", "遊戲結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    start = false;
                    return;
                }
            }

            // **檢查平手**
            if (count == 9)
            {
                MessageBox.Show("雙方平手，遊戲結束!", "遊戲結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                start = false;
            }
        }

        private int Minimax(int[,] board, int depth, bool isMaximizing)
        {
            int score = Evaluate(board);
            if (score == 10 || score == -10) return score;
            if (IsFull(board)) return 0;  // 平局

            if (isMaximizing)  // 電腦回合（最大化分數）
            {
                int best = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            board[i, j] = 1;
                            best = Math.Max(best, Minimax(board, depth + 1, false));
                            board[i, j] = 0;
                        }
                    }
                }
                return best;
            }
            else  // 玩家回合（最小化分數）
            {
                int best = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            board[i, j] = 10;
                            best = Math.Min(best, Minimax(board, depth + 1, true));
                            board[i, j] = 0;
                        }
                    }
                }
                return best;
            }
        }
        private int Evaluate(int[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    if (board[i, 0] == 1) return 10;  // 電腦贏
                    if (board[i, 0] == 10) return -10; // 玩家贏
                }
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    if (board[0, i] == 1) return 10;
                    if (board[0, i] == 10) return -10;
                }
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                if (board[0, 0] == 1) return 10;
                if (board[0, 0] == 10) return -10;
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                if (board[0, 2] == 1) return 10;
                if (board[0, 2] == 10) return -10;
            }

            return 0;
        }
        private bool IsFull(int[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == 0)
                        return false;
            return true;
        }


        void play()
         {
            if (!start || !computer) return;  // 確保遊戲開始且是電腦的回合

            int bestScore = int.MinValue;
            int bestRow = -1, bestCol = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state[i, j] == 0)  // 找到可下的位置
                    {
                        state[i, j] = 1;  // 假設電腦在這個位置下棋
                        int score = Minimax(state, 0, false);  // 計算此步驟的分數
                        state[i, j] = 0;  // 還原棋盤

                        if (score > bestScore)  // 找到更好的走法
                        {
                            bestScore = score;
                            bestRow = i;
                            bestCol = j;
                        }
                    }
                }
            }

            // 讓電腦下在最優位置
            if (bestRow != -1 && bestCol != -1)
            {
                state[bestRow, bestCol] = 1;
                count++;
                computer = false;
                showBoard();
            }/* if (!start || !computer) return;  // 確保遊戲開始且是電腦的回合

             // 1. 電腦能贏就直接獲勝
             for (int i = 0; i < 3; i++)
                 for (int j = 0; j < 3; j++)
                     if (state[i, j] == 0)
                     {
                         state[i, j] = 1;
                         if (checkWin(3)) { showBoard(); start = false; return; }
                         state[i, j] = 0; // 還原
                     }

             // 2. 防守：對方已經聽牌，立即攔截
             for (int i = 0; i < 3; i++)
                 for (int j = 0; j < 3; j++)
                     if (state[i, j] == 0)
                     {
                         state[i, j] = 10;
                         if (checkWin(30))
                         {
                             state[i, j] = 1;
                             count++;
                             computer = false;
                             showBoard();
                             return;
                         }
                         state[i, j] = 0; // 還原
                     }

             // 3. 積極進攻：找一個能讓電腦形成「雙聽牌」的點（電腦兩條線都可能贏）
             for (int i = 0; i < 3; i++)
                 for (int j = 0; j < 3; j++)
                     if (state[i, j] == 0)
                     {
                         state[i, j] = 1;
                         if (checkWin(2))  // 如果這步可以讓電腦進入「聽牌狀態」
                         {
                             count++;
                             computer = false;
                             showBoard();
                             return;
                         }
                         state[i, j] = 0; // 還原
                     }

             // 4. 佔據中間（中間是最有利的位置）
             if (state[1, 1] == 0)
             {
                 state[1, 1] = 1;
                 count++;
                 computer = false;
                 showBoard();
                 return;
             }

             // 5. 強制壓制玩家，嘗試干擾玩家行動（如果玩家選了角落，電腦選相對應位置）
             int[][] aggressiveMoves = {
         new int[] { 0, 0 }, new int[] { 0, 2 },
         new int[] { 2, 0 }, new int[] { 2, 2 }
     };
             foreach (var move in aggressiveMoves)
             {
                 int i = move[0], j = move[1];
                 if (state[i, j] == 0)
                 {
                     state[i, j] = 1;
                     count++;
                     computer = false;
                     showBoard();
                     return;
                 }
             }

             // 6. 選擇剩餘角落（優勢位置）
             int[][] corners = { new int[] { 0, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 }, new int[] { 2, 2 } };
             foreach (var corner in corners)
             {
                 int i = corner[0], j = corner[1];
                 if (state[i, j] == 0)
                 {
                     state[i, j] = 1;
                     count++;
                     computer = false;
                     showBoard();
                     return;
                 }
             }

             // 7. 最後選擇邊線（沒有更好選擇時）
             int[][] edges = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 2 }, new int[] { 2, 1 } };
             foreach (var edge in edges)
             {
                 int i = edge[0], j = edge[1];
                 if (state[i, j] == 0)
                 {
                     state[i, j] = 1;
                     count++;
                     computer = false;
                     showBoard();
                     return;
                 }
             }


             /*     if (!start) return;

                   // 1. 電腦差一步就能贏，直接獲勝
                   for (int i = 0; i < 3; i++)
                       for (int j = 0; j < 3; j++)
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 1;
                               if (checkWin(3)) { showBoard(); start = false; return; }
                               state[i, j] = 0; // 還原
                           }

                   // 2. 防守：對方已經聽牌，攔截
                   for (int i = 0; i < 3; i++)
                       for (int j = 0; j < 3; j++)
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 10;
                               if (checkWin(30))
                               {
                                   state[i, j] = 1;
                                   count++;
                                   computer = false;
                                   showBoard();
                                   return;
                               }
                               state[i, j] = 0; // 還原
                           }

                   // 3. 電腦選取後可以造成條線聽牌，下一步贏
                   for (int i = 0; i < 3; i++)
                       for (int j = 0; j < 3; j++)
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 1;
                               if (checkWin(2))
                               {
                                   count++;
                                   computer = false;
                                   showBoard();
                                   return;
                               }
                               state[i, j] = 0; // 還原
                           }

                   // 4. 電腦選取後可以造成條線聽牌，再破壞掉
                   for (int i = 0; i < 3; i++)
                       for (int j = 0; j < 3; j++)
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 1;
                               if (checkWin(2))
                               {
                                   state[i, j] = 10;
                                   if (!checkWin(30)) // 確保不讓對方直接獲勝
                                   {
                                       state[i, j] = 1;
                                       count++;
                                       computer = false;
                                       showBoard();
                                       return;
                                   }
                               }
                               state[i, j] = 0; // 還原
                           }

                   // 5. 先選正中央
                   if (state[1, 1] == 0)
                   {
                       state[1, 1] = 1;
                       count++;
                       computer = false;
                       showBoard();
                       return;
                   }

                   // 6. 再選角落
                   int[][] corners = { new int[] { 0, 0 }, new int[] { 0, 2 }, new int[] { 2, 0 }, new int[] { 2, 2 } };
                   foreach (var corner in corners)
                   {
                       int i = corner[0], j = corner[1];
                       if (state[i, j] == 0)
                       {
                           state[i, j] = 1;
                           count++;
                           computer = false;
                           showBoard();
                           return;
                       }
                   }

                   // 7. 最後選邊線
                   int[][] edges = { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 2 }, new int[] { 2, 1 } };
                   foreach (var edge in edges)
                   {
                       int i = edge[0], j = edge[1];
                       if (state[i, j] == 0)
                       {
                           state[i, j] = 1;
                           count++;
                           computer = false;
                           showBoard();
                           return;
                       }
                   } 
                  /*if (!start) return;

                  // 1. 電腦差一步就能贏，直接獲勝
                  for (int i = 0; i < 3; i++)
                  {
                      for (int j = 0; j < 3; j++)
                      {
                          if (state[i, j] == 0)
                          {
                              state[i, j] = 1;
                              if (checkWin(3)) // 檢查是否獲勝
                              {
                                  showBoard();
                                  start = false;
                                  return;
                              }
                              state[i, j] = 0; // 還原
                          }
                      }
                  }

                  // 2. 防守：對方已經聽牌，攔截
                  for (int i = 0; i < 3; i++)
                  {
                      for (int j = 0; j < 3; j++)
                      {
                          if (state[i, j] == 0)
                          {
                              state[i, j] = 10;
                              if (checkWin(30)) // 如果玩家要贏了，攔截
                              {
                                  state[i, j] = 1;
                                  count++;
                                  computer = false;
                                  showBoard();
                                  return;
                              }
                              state[i, j] = 0; // 還原
                          }
                      }
                  }

                  // 3. 電腦選取後可以造成條線聽牌，下一步贏
                  for (int i = 0; i < 3; i++)
                  {
                      for (int j = 0; j < 3; j++)
                      {
                          if (state[i, j] == 0)
                          {
                              state[i, j] = 1;
                              if (checkWin(2)) // 檢查是否能讓自己聽牌
                              {
                                  count++;
                                  computer = false;
                                  showBoard();
                                  return;
                              }
                              state[i, j] = 0; // 還原
                          }
                      }
                  }

                  // 4. 電腦選取後可以造成條線聽牌，再破壞掉
                  for (int i = 0; i < 3; i++)
                  {
                      for (int j = 0; j < 3; j++)
                      {
                          if (state[i, j] == 0)
                          {
                              state[i, j] = 1;
                              if (checkWin(2)) // 先讓自己聽牌
                              {
                                  state[i, j] = 10;
                                  if (!checkWin(30)) // 確保此點不會讓對方直接獲勝
                                  {
                                      state[i, j] = 1;
                                      count++;
                                      computer = false;
                                      showBoard();
                                      return;
                                  }
                              }
                              state[i, j] = 0; // 還原
                          }
                      }
                  }

                  // 5. 先選正中央
                  if (state[1, 1] == 0)
                  {
                      state[1, 1] = 1;
                      count++;
                      computer = false;
                      showBoard();
                      return;
                  }

                  // 6. 再選角落
                  int[][] corners = {
              new int[] { 0, 0 }, new int[] { 0, 2 },
              new int[] { 2, 0 }, new int[] { 2, 2 }
          };
                  foreach (var corner in corners)
                  {
                      int i = corner[0], j = corner[1];
                      if (state[i, j] == 0)
                      {
                          state[i, j] = 1;
                          count++;
                          computer = false;
                          showBoard();
                          return;
                      }
                  }

                  // 7. 最後選邊線
                  int[][] edges = {
              new int[] { 0, 1 }, new int[] { 1, 0 },
              new int[] { 1, 2 }, new int[] { 2, 1 }
          };
                  foreach (var edge in edges)
                  {
                      int i = edge[0], j = edge[1];
                      if (state[i, j] == 0)
                      {
                          state[i, j] = 1;
                          count++;
                          computer = false;
                          showBoard();
                          return;
                      }
                  }*/
            /*           if (!start) return;

                       // 1. 電腦差一步就能贏，直接獲勝
                       for (int i = 0; i < 3; i++)
                       {
                           for (int j = 0; j < 3; j++)
                           {
                               if (state[i, j] == 0)
                               {
                                   state[i, j] = 1;
                                   if (checkWin(3)) return;
                                   state[i, j] = 0; // 還原
                               }
                           }
                       }

                       // 2. 防守：對手差一步就能贏，攔截
                       for (int i = 0; i < 3; i++)
                       {
                           for (int j = 0; j < 3; j++)
                           {
                               if (state[i, j] == 0)
                               {
                                   state[i, j] = 10;
                                   if (checkWin(30))
                                   {
                                       state[i, j] = 1;
                                       count++;
                                       computer = false;
                                       showBoard();
                                       return;
                                   }
                                   state[i, j] = 0; // 還原
                               }
                           }
                       }

                       // 3. 選擇能讓自己聽牌的點
                       for (int i = 0; i < 3; i++)
                       {
                           for (int j = 0; j < 3; j++)
                           {
                               if (state[i, j] == 0)
                               {
                                   state[i, j] = 1;
                                   if (checkWin(2))
                                   {
                                       count++;
                                       computer = false;
                                       showBoard();
                                       return;
                                   }
                                   state[i, j] = 0; // 還原
                               }
                           }
                       }

                       // 4. 選擇能讓自己聽牌後再破壞對方的點
                       for (int i = 0; i < 3; i++)
                       {
                           for (int j = 0; j < 3; j++)
                           {
                               if (state[i, j] == 0)
                               {
                                   state[i, j] = 1;
                                   if (checkWin(2))
                                   {
                                       state[i, j] = 10;
                                       if (!checkWin(30)) // 如果此點不會讓對方直接贏
                                       {
                                           state[i, j] = 1;
                                           count++;
                                           computer = false;
                                           showBoard();
                                           return;
                                       }
                                   }
                                   state[i, j] = 0; // 還原
                               }
                           }
                       }

                       // 5. 優先選擇正中央
                       if (state[1, 1] == 0)
                       {
                           state[1, 1] = 1;
                           count++;
                           computer = false;
                           showBoard();
                           return;
                       }

                       // 6. 優先選擇角落
                       int[][] corners = {
               new int[] { 0, 0 },
               new int[] { 0, 2 },
               new int[] { 2, 0 },
               new int[] { 2, 2 }
           };
                       foreach (int[] corner in corners) // ✅ 現在 corner 是 int[]
                       {
                           int i = corner[0], j = corner[1]; // ✅ 正常運作
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 1;
                               count++;
                               computer = false;
                               showBoard();
                               return;
                           }
                       }

                       // 7. 最後選擇邊線
                       int[][] edges = {
               new int[] { 0, 1 },
               new int[] { 1, 0 },
               new int[] { 1, 2 },
               new int[] { 2, 1 }
           };
                       foreach (var edge in edges)
                       {
                           int i = edge[0], j = edge[1];
                           if (state[i, j] == 0)
                           {
                               state[i, j] = 1;
                               count++;
                               computer = false;
                               showBoard();
                               return;
                           }
                       }*/
        }
    void continueGame()
        {
            if (start && computer) // 確保遊戲還在進行並輪到電腦
            {
                Task.Delay(500).ContinueWith(t =>
                {
                    this.Invoke((Action)(() => play()));
                });
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[0, 0] == 0)
            {
                state[0, 0] = 10; // 玩家 X
                count++;
                computer = true;
                showBoard();
                play();
            }         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            int i, j;

            // ✅ 清空遊戲盤
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    state[i, j] = 0;
                    board[i, j].Text = ""; // ✅ 清空按鈕文字
                }
            }
            count = 0;   // ✅ 歸零計數器
            start = true; // ✅ 讓遊戲可以開始
            textBox1.Text = "請按開始";  // 提示玩家按開始
            showBoard(); // ✅ 重製畫面

            if (radioButton2.Checked == true)
            {
                computer = false;
                textBox1.Text = "玩家";
            }
            else
            {
                computer = true;
                textBox1.Text = "電腦";
                Task.Delay(200).ContinueWith(t =>
                {
                    this.Invoke((Action)(() => play()));
                });

            }

            showBoard(); // ✅ 確保畫面重製
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[0, 1] == 0)
            {
                state[0, 1] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[0, 2] == 0)
            {
                state[0, 2] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[1, 0] == 0)
            {
                state[1, 0] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[1, 1] == 0)
            {
                state[1, 1] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[1, 2] == 0)
            {
                state[1, 2] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[2, 0] == 0)
            {
                state[2, 0] = 10; count++; computer = true;
                   showBoard(); play();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[2, 1] == 0)
            {
                state[2, 1] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (start && !computer && state[2, 2] == 0)
            {
                state[2, 2] = 10; count++; computer = true;
                showBoard(); play();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }
        bool checkWin(int target)
        {
            // 檢查橫排與直排
            for (int i = 0; i < 3; i++)
            {
                if (state[i, 0] + state[i, 1] + state[i, 2] == target) return true;
                if (state[0, i] + state[1, i] + state[2, i] == target) return true;
            }
            // 檢查對角線
            if (state[0, 0] + state[1, 1] + state[2, 2] == target) return true;
            if (state[0, 2] + state[1, 1] + state[2, 0] == target) return true;

            return false;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
 }
