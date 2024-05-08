def is_sorted_palindrome_str(string_to_check:str) -> bool:
    return list(map(lambda i: True if string_to_check[i] == string_to_check[len(string_to_check) -i -1] and (i == 0 or (ord(string_to_check[i]) >= ord(string_to_check[i-1]))) else False, range(len(string_to_check)//2))).count(False) == 0 


def main():
    print(is_sorted_palindrome_str("abca"))
    print(is_sorted_palindrome_str("abccba"))
    print(is_sorted_palindrome_str("a"))
    print(is_sorted_palindrome_str("abcba"))

if __name__ == "__main__":
    main()
