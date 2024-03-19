# FishMarket

### API that services market rates of fishes.

<br>

## Setup
* Install Microsoft SQL Server Management Studio
* Install Visual Studio 2022

<br>

## Steps
* **git clone https://github.com/frkn2076/FishMarket**
* **Email Sender Configs:**
  * **Update appsettings to add sender email credentials.**
  * **Turn the "Allow less secure apps" on:**
    * Go to this link: https://support.google.com/a/answer/6260879?hl=en
  * **Update test smtp credentials as well: [here](https://github.com/frkn2076/FishMarket/blob/3540396fb6b3f9f2eb815d152b9b96f4c764e061/FishMarket.Test/ServiceTests/ServiceTestBuilder.cs#L19C17-L23C18)**
* **Migration:**
  * **Navigate to project folder: cd ..\FishMarket.Api**
  * **dotnet ef database update**
* **dotnet run**

## Api Documentation
* You can use swagger documentation about calling API.
* Or [Prepared Http file](https://github.com/frkn2076/FishMarket/blob/develop/FishMarket.Api/FishMarket.API.http) to use sample calls.
