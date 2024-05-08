def pythagorean_triplets(sum:int) -> None:
    for i in range(1, (sum + 1)//3 ):
        for j in range(i+1, ( sum+1 )//2):
            if is_pythagorean_triplet(i, j, sum):
                print(f"{i} < {j} < {sum - i - j}")


def is_pythagorean_triplet(a:int, b:int, sum:int):
    return sum * sum - 2*sum*(a+b) + 2*a*b == 0

def pythagorean_triplets_one_line(sum:int) -> None:
    [print(f"{i} < {j} < {sum - i - j}") for i in range(1, (sum + 1)//3) for j in range(i+ 1,(sum + 1)//2) if sum * sum - 2*sum*(i+j) + 2*i*j == 0]

def main():
    pythagorean_triplets(1000)
    pythagorean_triplets_one_line(1000)
    pythagorean_triplets(12)
    pythagorean_triplets_one_line(12)

if __name__ == "__main__":
    main()
