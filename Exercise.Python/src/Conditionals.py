import random
import sys
import os

age = 21

if age > 16 :
	print('you are old enought to drive')
else :
	print('you are not old enought to drive')

if age >= 21:
	print('you are old enough to drive a tractor')
elif age >= 16 :
	print('you are old enough to driva a car')
else : 
	print("You are not old enough to drive")

''' and or not '''


if ((age >= 1) and (age <= 18)) :
	print('your age is between 1 and 18 years')
elif (age == 21) or (age > 21) :
	print('You get a birthday')
else:
	print("Sorry")
