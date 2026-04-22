# Streszczenie problemu
W ramach Laboratorium 3 dotyczącym przetwarzania wielowątkowego w technologii .NET (C#) zrealizowano trzy główne zadania:
1. Zrównoleglenie algorytmu mnożenia macierzy przy wykorzystaniu wysokopoziomowej klasy `Parallel`.
2. Zrównoleglenie tego samego algorytmu z wykorzystaniem niskopoziomowej klasy `Thread`.
3. Stworzenie aplikacji okienkowej (technologia **Avalonia UI**), pozwalającej na asynchroniczne nakładanie 4 różnych filtrów graficznych na wczytany obraz przy pomocy wielowątkowości (`Thread`).

## Wyniki zrównoleglenia mnożenia macierzy
Aby zweryfikować poprawność i zmierzyć realne przyspieszenie działania algorytmu, wykonano testy dla dwóch rozmiarów kwadratowych macierzy wejściowych, używając do obliczeń wielowątkowych kilku procesów współbieżnych. Poniższe wyniki stanowią wartość uśrednioną (zgodnie ze specyfikacją zadania) zebraną w trybie Release odpowiednio z 5 prób pomiarowych dla macierzy 500x500 oraz 3 prób dla macierzy 1000x1000 w celu eliminacji przekłamań pojedynczych uruchomień.

### Test 1: Macierz 500x500 (4 wątki)
| Typ obliczeń | Średni czas wykonania |
| - | - |
| Obliczenia sekwencyjne | **234 ms** |
| Zrównoleglenie `Parallel.For` | **82 ms** |
| Zrównoleglenie z `Thread` | **93 ms** |

### Test 2: Macierz 1000x1000 (8 wątków)
| Typ obliczeń | Średni czas wykonania |
| - | - |
| Obliczenia sekwencyjne | **2102 ms** |
| Zrównoleglenie `Parallel.For` | **576 ms** |
| Zrównoleglenie z `Thread` | **598 ms** |

## Wnioski
- Implementacja wielowątkowa znacznie skróciła czas wykonywania skomplikowanych operacji (jak np. trzykrotna zagnieżdżona pętla `for` przy tradycyjnym mnożeniu macierzy). Różnice w czasach są tym bardziej wyraźne, im większy jest rozmiar danych. Przyspieszenie dla macierzy 1000x1000 wyniosło ponad x4 w stosunku do rozwiązania sekwencyjnego.
- W przypadku niskiego rozmiaru macierzy, zrównoleglenie niskopoziomowe za pomocą klasy `Thread` było nieznacznie szybsze, ze względu na mniejszy narzut wywołań wbudowanych w mechanizm `Parallel`. Z kolei przy większej porcji danych w teście dla 8 wątków wbudowany system zarządzania `Parallel.For` poradził sobie z rozłożeniem zadań wydajniej od manualnego równego podziału rzędów per `Thread`.
- Realizacja zadania z interfejsem graficznym potwierdziła, że delegowanie skomplikowanych operacji na pikselach (nakładanie filtrów) do osobnych wątków w tle chroni pętlę UI przed zablokowaniem (tzw. "zamrożeniem" okna) i skraca łączny czas nakładania zmian z racji równoczesnego działania poszczególnych modyfikatorów obrazu.