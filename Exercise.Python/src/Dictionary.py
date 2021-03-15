import random
import sys
import os

super_villians = {'Fiddler':'Isaac Bowin', 'Captain Cold' : 'Leonard Snart', 'Weather Wizard' : 'Mark Mardon'}

print(super_villians['Captain Cold'])

del super_villians['Fiddler']

super_villians['Captain Cold'] = 'Hartley Rathaway'

print(len(super_villians))

print(super_villians.get("Captain Cold"))

print(super_villians.keys())

print(super_villians.values())