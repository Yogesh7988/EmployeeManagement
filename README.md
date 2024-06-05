1. Change the Sql crediantial in appsetting.json
2. Run add-migration ypurmigrationname -context UserContext
3. Run Update-database -context UserContext
4. Run add-migration ypurmigrationname -context EmployeeContext
5. Run Update-database -context EmployeeContext
6. Now run application
