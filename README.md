
# Welcome to FoxBank
![FoxBank](https://user-images.githubusercontent.com/112638774/219429630-3ed1f98a-86cd-41b8-92f6-eb1436f50a37.gif)
## Contributors

<table>
  <tr>
    <td align="center"><a href="https://github.com/berkowicz"><img src="https://avatars.githubusercontent.com/u/112638774?v=4" width="100px;" alt="Daniel Bekowicz"/><br /><sub><b>Daniel Berkowicz</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/Rohnson95"><img src="https://avatars.githubusercontent.com/u/97821367?v=4" width="100px;" alt="Robert Johnson"/><br /><sub><b>Robert Johnson</b></sub></a><br />
    <td align="center"><a href="https://github.com/AkiVonAkira"><img src="https://avatars.githubusercontent.com/u/113895247?v=4" width="100px;" alt="AkiVonAkira"/><br /><sub><b>AkiVonAkira</b></sub></a><br /></td>
  </tr>
</table>

## About App

FoxBank is a comprehensive console banking application designed to facilitate a variety of financial operations, including account creation, deposits, and transfers. The application is powered by a Postgres database that securely stores all user account details and transaction records. Developed as a school project to enhance students' proficiency in C# programming and database connectivity, FoxBank is designed to offer a seamless and efficient banking experience to its users.

## The Code
|**Class**|**Breakdown**|
|-|-|
|Program.cs|Initialize program/login menu|
|Menu.cs|Contains method to print menu from any sized 1D array|
|Helper.cs|Contains methods we created for repeated use throughout the code|
|Transaction.cs|Contains methods that are used for transactions|
|TransactionHistory.cs|Contains method|
|PostgresDataAccess.cs|Contains all functions that communicates with the DB|
<br>


|**Objects**|**Breakdown**|
|-|-|
|AccountModel.cs|Manage bankaccount data|
|BankBranchModel.cs|Manage bank branch data|
|BankCurrencyModel.cs|Manage currency/exchangerates|
|BankLoanModel.cs|Manage loans|
|BankRoleModel.cs|Manage userrole admin or client|
|TransactionModel.cs|Manage transactions data|
|UserModel.cs|Manage user profile data|

## Scrumboard
<a href="https://trello.com/b/5cRIN4tA/fox-bank"><img src="https://user-images.githubusercontent.com/112638774/219671570-8045074d-0645-40b5-afb0-e1f30bcc21cb.PNG"/>

## C4 Model
![C4_model_Foxbank](https://user-images.githubusercontent.com/112638774/219667662-169e9c90-0468-4b84-a664-fda5e1041d8e.png)
