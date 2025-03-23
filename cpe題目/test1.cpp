#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>
using namespace std;
int a,b;
int f(int x,int y){
	if(x%b!=y%b)return x%b<y%b;
	else{
		if(abs(x)%2!=abs(y)%2)return x%2;
		if(x%2==0&&y%2==0)return x<y;
		else return x>y;
	}
}
int main(){
  while(cin>>a>>b){
  	vector<int>n(a);
  	for(auto&i:n)cin>>i;
  	cout<<a<<" "<<b<<endl;
  	sort(n.begin(),n.end(),f);
  	for(auto&i:n)cout<<i<<endl;
  }
}