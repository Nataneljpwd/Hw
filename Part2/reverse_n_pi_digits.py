

#using the Gregory-Leibniz Series, for each number we need to do one 10^n iterations
#the alg gives log10(n) (due to rounding errors the alg might give a digit within a range of 1) digits of pi where n is the number of iterations
# definatelly not the most efficient way but a neat oneliner
#runs in O(n^10)
def reverse_n_pi_digits(n:int) -> str:
    return f"{{:.{n}f}}".format((sum([(-1)**i/(2*i + 1) for i in range(n**10)]) * 4))[::-1]

def main():
    print(reverse_n_pi_digits(5)) # takes around 3 seconds


if __name__ == "__main__":
    main()
