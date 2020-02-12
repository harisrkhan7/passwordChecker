# passwordChecker
An API endpoint to check password strength and a console application to consume it. 

## Password Checking Criteria

Password strength is determined based on the presence of one or more groups of characters and password length. The groups being:- 

1. Lowecase Characters (i.e. a-z)
2. Uppercase Characters (i.e. A-Z)
3. Digits (i.e. 0-9)
4. Special Characters (e.g. -=[]\;,./~!@#$%^&*()_+{}|:<>?)

**Note: A special character is either of punctuation symbol, whitespace or symbol according to UTF-8 definition.**

Following are the categories of password strength with their set criteria:-

### 1. Blank
The password does not contain any characters i.e. null. 

### 2. Weak Password
* Less than **8** characters in length OR  
* Has characters from only **1** of the **4** groups

### 3. Medium Password
* **8** or more characters in length
* Has characters from at least **2** of the **4** groups

### 4. Strong Password
* **8** or more characters in length
* Has characters from at least **3** of the **4** groups

### 5. Very Strong Password
* **8** or more characters in length
* Has characters from all **4** of the **4** groups

