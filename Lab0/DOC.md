# Streszczenie problemu
Na zajęciach do wykonania był, prosty projekt sprawdzający umiejętności studentów. W dokumencie opisującym problem tych zajęć znalazł się problem wykonania klasy *FizzBuzz*
Klasa ta znajduje się w pliku [FizzBuzz.cs](./Lab0/FizzBuzz.cs).\
Głownym elementem do zaimplementowania była funkcja `Run()`, która miała dla wybranej liczby iterować do wskazanej przez użytkownika liczby, a następnie co trzeci wypisać `Fizz`, co piąty `Buzz`, a co piętnasty (podzielny przez 3 i 5) `FizzBuzz`.  
Poniżej znajduje się implementacja tej metody:
```c#
 public void Run() {
            for (int i = 1; i <= _topNumber; i++) {
                if (i % 3 == 0 && i % 5 == 0) {
                    Console.WriteLine("FizzBuzz");
                } else if (i % 3 == 0) {
                    Console.WriteLine("Fizz");
                } else if (i % 5 == 0) {
                    Console.WriteLine("Buzz");
                } else {
                    Console.WriteLine(i);
                }
            }
```
