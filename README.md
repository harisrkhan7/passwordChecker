# passwordChecker
An API endpoint to check password strength and a console application to consume it. 

## 1. Password Checking Criteria

Password strength is determined based on the presence of one or more groups of characters and password length. The groups being:- 

1. Lowecase Characters (i.e. a-z)
2. Uppercase Characters (i.e. A-Z)
3. Digits (i.e. 0-9)
4. Special Characters (e.g. -=[]\;,./~!@#$%^&*()_+{}|:<>?)

**Note: A special character is either of punctuation symbol, whitespace or symbol according to UTF-8 definition.**

Following are the categories of password strength with their set criteria:-

### 1.1 Blank
The password does not contain any characters i.e. null. 

### 1.2 Weak Password
* Less than **8** characters in length OR  
* Has characters from only **1** of the **4** groups

### 1.3 Medium Password
* **8** or more characters in length
* Has characters from at least **2** of the **4** groups

### 1.4 Strong Password
* **8** or more characters in length
* Has characters from at least **3** of the **4** groups

### 1.5 Very Strong Password
* **8** or more characters in length
* Has characters from all **4** of the **4** groups

## 2. Branching Structure

The baseline version and the improved version are both in a runnable state for comparison in two different branches stated as follows:- 

* Task 1 - Master Branch
* Task 2 - feature/checkDataBreach Branch

## 3. Program Running Instructions

The solution file has been setup to run multiple projects at once. Cloning any branch followed by building and running the solution locally would:-

* Launch the web application at the set ports
* Launch the console application and connect it to the web application

**Note**: Each branch also has a test project for unit testing controllers of that branch. The unit tests were designed using Boundary Value Analysis to ensure maximum coverage.

### 3.1 Makefile

A basic makefile with the following commands has been setup to aid development:- 

* **make run_webapi** to run Web API only
* **make run_console_application** to run Console Application only
* **make test_webapi** to run all the Web API tests 
* **make generate_client** to generate a new client from the swaager.json. This would require Node and Autorest to be installed on the local machine. 

## 4. Compatiblity 

Follwing projects target both .Net Standard 2.1 and .Net Core 3.1 frameworks:-

* All the WebAPI Libraries
* The Web API Client Library
* The Console Application

The following projects target .Net Core 3.1 framework as it is the latest

* The Web API 
* The Functional Tests for the Web API 

**Note:** The core parts of the projects supporting both frameworks ensures maximum flexibility.

## 5. Known Improvements

### 5.1 Client Usage

* The client was generated using AutoRest and updated in each branch. This can be automated by features like generate on each build. 
* The client project was directly referenced by the Console Application. This can be avoided by the use of **NuGet package** in an **Enterprise NuGet Feed**.



