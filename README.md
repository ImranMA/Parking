<h3>Parking Calculator</h3>

Write a rate calculation engine for a carpark, the inputs for this engine are:
1. Patron’s Entry Date and Time
2. Patron’s Exit Date and Time
</br>
Based on these 2 inputs the engine program should calculate the correct rate for the patron and display the name of
the rate along with the total price to the patron using the following rates:</br></br>
<b>Name of the Rate</b></br>
Early Bird</br>
Type Flat Rate</br>
Total Price $13.00</br>
Entry condition Enter between 6:00 AM to 9:00 AM</br>
Exit condition Exit between 3:30 PM to 11:30 PM</br>
</br></br>
<b>Name of the Rate</b> </br>
Night Rate </br>
Type Flat Rate
Total Price $6.50 </br>
Entry condition Enter between 6:00 PM to midnight (weekdays) </br>
Exit condition Exit before 6 AM the following day </br>
</br></br>
<b>Name of the Rate</b> </br>
Weekend Rate</br>
Type Flat Rate </br>
Total Price $10.00 </br>
Entry condition Enter anytime past midnight on Friday to Sunday </br>
Exit condition Exit any time before midnight of Sunday </br></br>
Note: If a patron enters the carpark before midnight on Friday and if they qualify </br>
for Night rate on a Saturday morning, then the program should charge the</br>
night rate instead of weekend rate.</br>
</br></br>
For any other entry and exit times the program should refer the following table for calculating the total price.</br>
<b>Name of the Rate</b> </br> Standard Rate</br>
Type Hourly Rate</br>
0 – 1 hours $5.00</br>
1 – 2 hours $10.00</br>
2 – 3 hours $15.00</br>
3 + hours $20.00 flat rate per day for each day of parking.</br>
