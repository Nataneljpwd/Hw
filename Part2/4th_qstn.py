from typing import Tuple
from matplotlib import pyplot as plt
import numpy as np


def main():
    lst, num_pos_numbers, sum = input_numbers() ; lst.sort()
    print(f"List of sorted numbers: {lst}")
    print(f"Number of positive numbers: {num_pos_numbers}")
    print(f"Average of all numbers: {sum/len(lst)}")

    x = [i for i in range(len(lst))] ; y = lst
    coef = np.corrcoef(x,y)
    f, ax = plt.subplots()
    plt.text(0.01,0.99, f"Correlation Coefficient: {coef[0][1]}", transform=ax.transAxes, ha='left', va='top')
    plt.scatter(x, y)
    plt.show()


def input_numbers() -> Tuple[list[float], int, float]:
    """Returns a tuple containing the list of numbers. the number or positive numbers and the sum of all numbers"""
    a = float(input("Enter a number: "))
    lst = []
    num_pos_numbers = 0
    sum = 0.0
    while a != -1:
        lst.append(a)
        if a > 0:
            num_pos_numbers += 1
        sum += a
        a = float(input("Enter a number: "))
    return (lst, num_pos_numbers, sum)

if __name__ == "__main__":
    main()
