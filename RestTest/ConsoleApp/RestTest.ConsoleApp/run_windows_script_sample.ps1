.\RestTest.ConsoleApp.exe -c .\Tests\crud_test.json > result.txt
.\RestTest.ConsoleApp.exe -c .\Tests\body_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\Tests\header_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\Tests\cookies_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c .\Tests\query_string_test.json >> result.txt
.\RestTest.ConsoleApp.exe -c -v .\Tests\fail_test.json >> result.txt
