
### Mates-Rates Rent-a-Car (MRRC) User Manual ###

Author: Ash Phillips (n10477659)
Date: 03/06/20

This program has many functionalities, and will allow the operator to do a number of processes to manage a vehicle rental management system.
It is important to follow the instructions detailed in the manual carefully so that the program behaves as expected.


## Building:

To build the code for this program, navigate to the "MRRC.sln" file in the root folder of the project and open it with Visual Studio.
In the "Solution Explorer" toolbar, expand the "MRRC" solution,
right-click the "MRRC" project and select "Build", also right-click the "MRRC_Management" project and again select "Build". 


## Running:

To run the program press the "f5" key on the keyboard after building the project.
This will open a new console window from which the program will run.
You should be met with the following greeting message,
followed by the main menu:

```
    ### Mates-Rates Rent-a-Car Operation Menu ###

    You may press the ESC key at any menu to exit.

    Press the BACKSPACE key to return to the previous menu. Press the H key to return to the main menu.
```

For the tables to display correctly later on, please right-click on the command window header, where the file path is displayed.
Next select "Properties". A properties window should then pop up.
Next go to the "Layout" tab, and here uncheck the box "Wrap text output on resize".
Then press "OK" to finish setup.


## Custom Files:

By default, the program data is stored within appropriately labeled comma-separated value (CSV) files located in "Data" folder(inside the root folder).
Namely: 

* "fleet.csv" - Vehicle fleet data
* "customers.csv" - Customer information data
* "rentals.csv" - Vehicle and customer rental data

If the user wishes to use files stored elsewhere they should be
specified as command line arguments.
To do this right-click the "MRRC" project in the "Solution Explorer" and, select the "Debug" tab in the newly opened file and paste the following into the "command line arguments" text box:

```
    "path\to\fleet\csv" "path\to\rentals\csv" "path\to\customers\csv"
```

Note that you will need to replace the dummy paths with paths to the files you wish to use in the program (relative or absolute).
Once you have done this, you man run the program as described before.


## Program Usage:

This program is comprised of a hierachal set of menus.
You will start at the "Main Menu", and depending on your choice you will be sent to different sub-menus, all focused on a specific aspect of the system.

As specified in the greeting message you may press the "ESC" key at any menu to exit the program, the "BACKSPACE" key to return to the previous menu, and the "H" key to return to the main menu.


### Main Menu:

When you enter the program the following main menu will be displayed after the greeting message:

```
    Please enter a letter from the options below:

    a) Customer Management
    b) Fleet Management
    c) Rental Management
```

After you input a number("a", "b", or "c") the program will enter the corresponding sub-menu (there is no need to press enter on any of the menus, the program is expecting only a single character).


### Customer Management Menu:

When you enter "a" on the "Main Menu" the following menu will be
displayed. You will note that each of the options on the menu
correspond to a core process relating to the mangement of customers.

```
    Please enter a letter from the options below:

    a) Display Customers
    b) New Customer
    c) Modify Customer
    d) Delete Customer
```

After you input a character ("a", "b", "c", or "d") the program will enter a small prompt based sequence to assist you in completing your task.


#### Display Customers:

This sequence will very simpily draw a table to the console containing details for every customer in the CRM.
It may look something like this:

```
    ------------------------------------------------------------
    | ID | Title | FirstName | LastName  | Gender | DOB        |
    ------------------------------------------------------------
    | 0  | Ms    | Holly     | Hutson    | Female |  7/01/1990 |
    | 1  | Ms    | Beverley  | Woodrow   | Female |  8/07/1987 |
    | 2  | Mr    | Albert    | Audra     | Male   | 11/12/1930 |
    | 3  | Mrs   | Ami       | Berry     | Female |  9/01/1999 |
    | 4  | Count | Fredrich  | von Trapp | Male   | 14/03/1997 |
    ------------------------------------------------------------
```

If there are no cutomers to display to program will output the message:

```
	There are no cutsomers to display.
```


#### New Customer:

This sequence will give prompts to the user to fill in the details.
During this sequence the ESC and BACKSPACE commands will be disabled as the fields must be filled in before the sequence can be finished.

The output will be:

```
	*** ESC and BACKSPACE commands disabled, must fill field(s) ***

	Please fill the following field(s). (fields marked with * are required)

	*Title:
```

When entering details user must press ENTER to enter next detail.
Should look somehing like this:

```
	*Title: Mrs
	*First Name: Jane
	*Last Name: Smith
	*Gender (Male/Female/Other): Female
	*DOB (dd/MM/yyyy): 02/01/2000
```

When entering last detail the cutomer will be added to the data.
The following message will be printed and the user will be 
redirected to the previous menu:

```
	Successfully created new cutomer '0 - Mrs Jane Smith' and added to customers list.
```

If any detail is entered incorrectly the user will get the follwing message and be redirected to filling in the prompts:

```
	*** WARNING: Failed to create new customer. Please enter valid details. ***
```


#### Modify Customer:

This sequence will give a prompt to the user to enter the ID number:

```
	*** ESC and BACKSPACE commands disabled, must fill field(s). ***
	Please enter the ID number of the customer to modify.
```

During this sequence the ESC and BACKSPACE commands will be disabled as the fields must be filled in before the sequence can be finished.
When entering details user must press ENTER to enter next detail.

Once ID is entered the output will look something like the following:

```
	*** ESC and BACKSPACE commands re-enabled. ***

	Please enter the number assigned to the detail to modify. To modify all details at once enter +.

	ID Number (CANNOT MODIFY): 0
	a) Title: Mrs
	b) First Name: Jane
	c) Last Name: Smith
	d) Gender (Male/Female/Other): Female
	e) DOB (dd/MM/yyyy): 02/01/2000
```

One a valid input is entered, the console will output prompts in the same format as the New Customer sequence.

When all fields are filled in and the customer is successfully modified, the console will redirect to the previous menu.


### Delete Customer:

This sequence will give a prompt to the user to enter the ID number in the same format as the Modify Customer sequence.

Once ID is entered the output will look something like the following:

```
	*** ESC and BACKSPACE commands re-enabled. ***

	Please enter the number assigned to the detail to modify. To modify all details at once enter +.

	ID Number (CANNOT MODIFY): 0
	a) Title: Mrs
	b) First Name: Jane
	c) Last Name: Smith
	d) Gender (Male/Female/Other): Female
	e) DOB (dd/MM/yyyy): 02/01/2000	

	To confirm deletion enter (Y)es. To go back enter (N)o.
```

If the user enters "N", the console will redirect to the ID request.
If the user enters "Y", the console will print the following message and redirect to the previous menu:

```
	Successfully deleted customer '11 - Mrs Jane Smith' and removed from customers list.
```


### Fleet Management Menu:

When you enter "b" on the "Main Menu" the following menu will be
displayed. You will note that each of the options on the menu
correspond to a core process relating to the mangement of vehicles.

```
    Please enter a letter from the options below:

    a) Display Fleet
    b) New Vehicle
    c) Modify Vehicle
    d) Delete Vehicle
```

After you input a character ("a", "b", "c", or "d") the program will enter a small prompt based sequence to assist you in completing your task.


#### Display Vehicles:

This sequence will very simpily draw a table to the console containing details for every vehicle in the Fleet.
It will be presented in the smae format as the Customer table.

If there are no vehicles to display to program will output the message:

```
	There are no vehicles to display.
```


#### New Vehicle:

This sequence will give prompts to the user to fill in the details.
During this sequence the ESC and BACKSPACE commands will be disabled as the fields must be filled in before the sequence can be finished.

First, the user will be asked to choose to enter all fields, or only what is required.
The output will be:

```
	Please enter a letter from the options below.

	a) Fill all fields
	b) Only fill required fields
```

If the user choose "a", the console will give prompts for all fields.
If "b", there will only be prompts for the required fields.
When entering details user must press ENTER to enter next detail.
Should look somehing like this:

```
	*** ESC and BACKSPACE commands disabled, must fill feild(s). ***

	Please fill the following field(s). (fields marked with * are required)
	
	*Registration: 123ABC
	*Grade (Economy/Family/Luxury/Commercial): Family
	*Make: Toyota
	*Model: Rav 4
	*Year: 2006
	Number of Seats: 5	
	Transmission Type (Automatic/Manual): Automatic
	Fuel Type (Petrol/Diesel): Diesel
	GPS (Y/N): Y
	Sunroof (Y/N): N
	Daily Rate: $70
	Colour: White
```

When entering last detail the vehicle will be added to the data.
The following message will be printed and the user will be 
redirected to the previous menu:

```
	Successfully created new vehicle '123ABC - Toyota Rav 4 2006" and added to vehicle list.
```

If any detail is entered incorrectly the user will get the follwing message and be redirected to filling in the prompts:

```
	*** WARNING: Failed to create new vehicle. Please enter valid details. ***
```


#### Modify Vehicle:

This sequence will give a prompt to the user to enter the registration number:

```
	*** ESC and BACKSPACE commands disabled, must fill field(s). ***
	Please enter the registration number of the vehicle to modify.
```

During this sequence the ESC and BACKSPACE commands will be disabled as the fields must be filled in before the sequence can be finished.
When entering details user must press ENTER to enter next detail.

Once the registration is entered the output will look something like the following:

```
	*** ESC and BACKSPACE commands re-enabled. ***

	Please enter the number assigned to the detail to modify.
     		OR
	To modify all details at once enter +. To modify only all required details at once enter *.

	        a) Registration: 123ABC
	        b) Grade (Economy/Family/Luxury/Commercial): Family
	        c) Make: Toyota
	        d) Model: Rav 4
	        e) Year: 2006
	        f) Number of Seats: 5
	        g) Transmission Type (Automatic/Manual): Automatic
	        h) Fuel Type (Petrol/Diesel): Diesel
	        i) GPS (Y/N): True
	        j) Sunroof (Y/N): False
	        k) Daily Rate: 70
	        l) Colour: White
```

One a valid input is entered, the console will output prompts in the same format as the New Vehicle sequence.

When all fields are filled in and the vehicle is successfully modified, the console will redirect to the previous menu.


### Delete Vehicle:

This sequence will give a prompt to the user to enter the registration number in the same format as the Modify Vehicle sequence.

Once registration is entered the output will look something like the following:

```
	*** ESC and BACKSPACE commands re-enabled. ***

	Vehicle Details:

        a) Registration: 123ABC
        b) Grade (Economy/Family/Luxury/Commercial): Family
        c) Make: Toyota
        d) Model: Rav 4
        e) Year: 2006
        f) Number of Seats: 5
        g) Transmission Type (Automatic/Manual): Automatic
        h) Fuel Type (Petrol/Diesel): Diesel
        i) GPS (Y/N): True
        j) Sunroof (Y/N): False
        k) Daily Rate: 70
        l) Colour: White

	To confirm deletion enter (Y)es. To go back enter (N)o.
```

If the user enters "N", the console will redirect to the
registration request.
If the user enters "Y", the console will print the following message and redirect to the previous menu:

```
	Successfully deleted vehicle '123ABC - Toyota Rav 4 2006' and removed from vehicles list.
```


### Rental Management Menu:

When you enter "c" on the "Main Menu" the following menu will be
displayed. You will note that each of the options on the menu
correspond to a core process relating to the mangement of rentals.

```
    Please enter a letter from the options below:

    a) Display Rentals
    b) Search Vehicles
    c) Rent Vehicle
    d) Return Vehicle
```

After you input a character ("a", "b", "c", or "d") the program will enter a small prompt based sequence to assist you in completing your task.


#### Display Vehicles:

This sequence will very simpily draw a table to the console containing details for every rental in the Rentals file and the daily rate of the rental.
It will be presented in the same format as the Customer table.

If there are no rentals to display to program will output the message:

```
	There are no rentals to display.
```


### Search Vehicles:

This sequence will give a prompt to the user to enter a search query in the format:
eg. Blue OR Red AND Economy

```
	*** ESC and BACKSPACE commands disabled, must fill feild(s). ***

	To search for a vehicle please type in the attribute you want to search for.

	To search for more than one attribute please use only the AND, OR, or () operators.

	If the searched attribute has spaces please put quotations (") around it.

	Search vehicles >>> 
```

Once the user tyes in a query, a table of unrented vehicles with the attributes in the query will display and the user will be returned to the previous menu.
If there are errors in the query, error messages will the display and the user will be sent back to the user input for the query.


### Rent Vehicle:

This sequence will give a prompt to the user to enter the ID, registration number, and days renting in the below format:

```
	*** ESC and BACKSPACE commands disabled, must fill feild(s). ***

	Please fill the following field(s). (fields marked with * are required)

	To escape sequence, please leave field(s) blank.

	*ID: 1
	*Registration: 851VOL
	*Rental Time (days): 5

	The total cost of this rental  will be $185.56. Please enter (Y)es to proceed or (N)o to go back.

	>
```

Once fields are entered the total cost message displays and the user can either continue or go back.
If the user enters N, the rent vehicle menu with reload and go bck to the user inputs.
If the user enters "Y", the console will print the following message and redirect to the previous menu:

```
	Successfully rented vehicle '851VOL' to customer '1' and added to rentals list.

```


### Return Vehicle:

This sequence will give a prompt to the user to enter the registration number in the below format:

```
	*** ESC and BACKSPACE commands disabled, must fill feild(s). ***

	Please enter the registration number of the vehicle to return.

	To escape sequence, please leave field blank.

	> 851VOL
```

Once fields are entered, the user is asked to confirm return.
If the user enters N, the return vehicle menu with reload and go bck to the user inputs.
If the user enters "Y", the console will print the following message and redirect to the previous menu:

```
	Successfully returned vehicle '851VOL' from customer '1' and removed from rentals list.

```


