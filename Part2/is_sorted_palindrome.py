def is_sorted_palindrome_str(p:str) -> bool:
    return list(map(lambda i: True if p[i] == p[len(p) -i -1] and (i == 0 or (ord(p[i]) >= ord(p[i-1]))) else False, range(len(p)//2))).count(False) == 0 


def main():
    print(is_sorted_palindrome_str("abca"))
    print(is_sorted_palindrome_str("abccba"))
    print(is_sorted_palindrome_str("a"))
    print(is_sorted_palindrome_str("abcba"))

if __name__ == "__main__":
    main()
