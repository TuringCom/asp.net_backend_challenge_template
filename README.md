# Turing Back End Challenge
To complete this challenge, you need to ensure all route returns a similar response object as described in our API guide. To achieve this goal, you will have to fix the existing bugs, implement the incomplete functions, and add test cases for the main functions of the system.

## Getting started

### Prerequisites

In order to install and run this project locally, you would need to have the following installed on you local machine.

* [**.NET Core SDK 2.2**](https://dotnet.microsoft.com/download/dotnet-core/2.2)
* [**MySQL**](https://www.mysql.com/downloads/)


### Installation

* Clone this repository
* Navigate to the project directory `cd src/`
* Create a virtual environment
* Install dependencies `dotnet restore`

* Edit `src\TuringBackend.Api\appsettings.json` database credentials to your database instance

* Create a MySQL database and run the sql file in the database directory to migrate the database
`mysql -u <dbuser> -D <databasename> -p < ./sql/database.sql`

* Run development server

`dotnet run`		

## Request and Response Object API guide for all Endpoints

* Check [here](https://docs.google.com/document/d/1J12z1vPo8S5VEmcHGNejjJBOcqmPrr6RSQNdL58qJyE/edit?usp=sharing)
* Visit `http://127.0.0.1:80/docs/

## Using Docker 
Build image

`docker build -t turing_app .` 

Run Image
`docker run --rm -p 80:80 turing_app`

