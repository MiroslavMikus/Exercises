import random
import sys
import os

grocery_list = ['Juice','Tomates','Potatos','Bananas']

print('First item is ',grocery_list[0])

grocery_list[0] = 'Green Juice'

print('First item is ',grocery_list[0])

print(grocery_list[1:3])

other_events= ['Wash Car','Pick Up Kids', 'Cash Check']

to_do_list = [other_events, grocery_list]

print(to_do_list[1][1])

grocery_list.append('onions')

grocery_list.insert(1,"Pickle")

grocery_list.remove("Pickle")

grocery_list.sort()

grocery_list.reverse

print(grocery_list)

del grocery_list[1:2]

print(grocery_list)

'''Combine list'''

to_do_list2 = other_events + grocery_list

print(len(to_do_list2))

print(max(to_do_list2))

print(min(to_do_list2))