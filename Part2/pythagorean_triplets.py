

#we have 2 equations, a^2 + b^2 = c^2 and a + b + c = sum
#after we solve those we get 0 = n^2 - 2n(a+b) + 2ab so what we do is loop over the range of n and when the equation is true, we print the triplet
def pythagorean_triplets(sum:int) -> None:
    #the largest number a can be is (sum + 1)//3 because a + b + c = sum and a is the smallest number
    for i in range(1, (sum + 1)//3 ):
        #the largest number b can be is (sum - i)//2 because c needs to be larger than both a and b (worst case is that b = sum/2 - a (where a is 1) and c needs to be larger than the largest number b can be)
        for j in range(i+1, ( sum+1 )//2):
            if is_pythagorean_triplet(i, j, sum):
                print(f"{i} < {j} < {sum - i - j}")

def pythagorean_triplets_one_line(sum:int) -> None:
    [print(f"{i} < {j} < {sum - i - j}") for i in range(1, (sum + 1)//3) for j in range(i+ 1,(sum + 1)//2) if sum * sum - 2*sum*(i+j) + 2*i*j == 0]

def is_pythagorean_triplet(a:int, b:int, sum:int):
    return sum * sum - 2*sum*(a+b) + 2*a*b == 0

def main():
    pythagorean_triplets(1000)
    pythagorean_triplets_one_line(1000)
    pythagorean_triplets(12)
    pythagorean_triplets_one_line(12)

if __name__ == "__main__":
    main()
