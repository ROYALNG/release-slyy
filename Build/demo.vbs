sub main()
'读变量值：Server.GetValue("通道ID","控制器ID","变量ID")
'写变量值：Server.SetValue("通道ID","控制器ID","变量ID","12")
 'add your code here.  
dim a
dim b
dim c
dim d
  a=cint(Server.GetValue("1","1","1"))
  b=cint(Server.GetValue("1","1","2"))
  c=cint(Server.GetValue("1","1","3"))
  d=a+b+c
 Server.SetValue("1","1","4",d)
end sub