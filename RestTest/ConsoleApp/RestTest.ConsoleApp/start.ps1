.\RestTest.ConsoleApp.exe -c .\crud_test.json > result.txt
.\RestTest.ConsoleApp.exe -c .\body_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\header_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\cookies_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\query_string_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c -v .\fail_test.json >> result.txt
