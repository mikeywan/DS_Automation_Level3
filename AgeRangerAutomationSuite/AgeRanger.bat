@echo off

cd C:\Code_Root\Git\DebitSuccess\DS_Automation_Level3\AgeRangerAutomationSuite\
nunit3-console.exe --labels=All  /out=TestResult.txt "--result=TestResult.xml;format=nunit2"  bin\Debug\AgeRangerWebUi.dll
..\packages\SpecFlow.2.2.1\tools\specflow.exe nunitexecutionreport AgeRangerTestSuite.csproj  /out:AgeRangerResult.html

set hour=%time:~0,2%
if "%hour:~0,1%" == " " set hour=0%hour:~1,1%
set min=%time:~3,2%
if "%min:~0,1%" == " " set min=0%min:~1,1%
set secs=%time:~6,2%
if "%secs:~0,1%" == " " set secs=0%secs:~1,1%

set year=%date:~6,4%
set month=%date:~0,2%
if "%month:~0,1%" == " " set month=0%month:~1,1%
set day=%date:~3,2%
if "%day:~0,1%" == " " set day=0%day:~1,1%

set datetimef=%year%-%month%-%day%_%hour%%min%%secs%

mailsend.exe -list-address mailreceiver.list -from mikey.wan@outlook.com -starttls -port 587 -auth -smtp smtp-mail.outlook.com -sub AgeRangerReport-%datetimef% -cs "utf-8" -mime-type "text/html" -msg-body "AgeRangerResult.html"  -v -user mikey.wan@outlook.com -pass "************" -ehlo
