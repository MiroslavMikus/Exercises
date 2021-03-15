import random
import sys
import os


class Animal:
    __name = None
    __height = 0
    __weight = 0
    __sound = 0

    def set_name(self, name):
        self.__name = name

    def get_name(self):
        return self.__name

    def set_height(self, height):
        self.__height = height

    def get_height(self):
        return self.__height

    def set_weight(self, weight):
        self.__weight = weight

    def get_weight(self):
        return self.__weight

    def set_sound(self, sound):
        self.__sound = sound

    def get_sound(self):
        return self.__sound

    def __init__(self, name, height, weight, sound):
        self.__name = name
        self.__height = height
        self.__weight = weight
        self.__sound = sound

    def get_type(self):
        print("Animal")

    def toString(self):
        return "{} is {} cm tall and {} kilograms and say {}".format(self.__name,
                                                                     self.__height,
                                                                     self.__weight,
                                                                     self.__sound)

    def multiple_sounds(self, how_many=None):
        if how_many == None:
            print(self.__sound)
        else:
            print((self.__sound + "\n") * how_many)

class Dog(Animal):
    __owner = ""

    def set_owner(self, owner):
        self.__owner = owner

    def get_owner(self):
        return __owner

    def __init__(self, name, height, weight, sound, owner):
        self.__owner = owner
        super(Dog, self).__init__(name, height, weight, sound)

    def get_type(self):
        print("Dog")

    def toString(self):
        return super().toString() + " His owner is " + self.__owner

cat = Animal("Wiskers",33,10, "Meow")

print(cat.toString())

dog = Dog("Spot", 53, 27, "Ruff", "Mirec")

print(dog.toString())

cat.multiple_sounds()

dog.multiple_sounds(3)

class AnimalTesting:
    def get_type(self, animal):
        animal.get_type()

test_animals = AnimalTesting()

test_animals.get_type(cat)
test_animals.get_type(dog)