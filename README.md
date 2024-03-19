# FishMarket

### API that services market rates of fishes.

<br>

## Setup
* Install Microsoft SQL Server
* Install Visual Studio 2022 (.Net 8 SDK)

<br>

## Steps
* **git clone https://github.com/frkn2076/FishMarket**
* **Update connection string on appsettings to replace default user password or set your password on connection string as your default user password on database, please [see](https://github.com/frkn2076/FishMarket/blob/f4576ecf93e7071a24c4d239754b4acb8399e7e4/FishMarket.Api/appsettings.Development.json#L18C73-L18C83).**
* **Email Sender Configs:**
  * **Update appsettings to add sender email credentials.**
  * **Turn the "Allow less secure apps" on:**
    * Go to this link: https://support.google.com/a/answer/6260879?hl=en
  * **Update test smtp credentials as well: [here](https://github.com/frkn2076/FishMarket/blob/3540396fb6b3f9f2eb815d152b9b96f4c764e061/FishMarket.Test/ServiceTests/ServiceTestBuilder.cs#L19C17-L23C18).**
* **Migration:**
  * **Navigate to project folder: cd ..\FishMarket.Api**
  * **dotnet ef database update**
* **dotnet run**

## Api Documentation
* You can use swagger documentation about calling API.
* Or [Prepared Http file](https://github.com/frkn2076/FishMarket/blob/develop/FishMarket.Api/FishMarket.API.http) to use sample calls.
