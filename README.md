# NCore

A simple library for functional writing

# Information

The library contains some basic methods and classes that make it easy to write hubrid: functionally and objectively

# Usage

## New collections

```C#
MutableArray
```
Methods
```C#
NOptional<T> Get(int index)
IEnumerator<T> GetEnumerator()
T this[int index]
MutableArray<T> Of(int count)
MutableArray<T> Of(IEnumerable<T> collecion)
MutableArray<T> Of(T[] array)
MutableArray<T> GetElements(int startIndex, int count)
MutableArray<T> Take(int take)
MutableArray<T> Skip(int skip)
NOptional<T> First()
NOptional<T> Last()
```

## Delegates

```C#
bool TryParse<T>(string value, out T result)
bool TryParseIgnoreCase<T>(string value, bool ignoreCase, out T result)
```

## Generator (beter random)

```C#
IGen

char NextChar();
char NextChar(char maxValue);
char NextChar(char minValue, char maxValue);
sbyte NextSByte();
sbyte NextSByte(int maxValue);
sbyte NextSByte(int minValue, int maxValue);
byte NextByte();
byte NextByte(int maxValue);
byte NextByte(int minValue, int maxValue);
short NextShort();
short NextShort(int maxValue);
short NextShort(int minValue, int maxValue);
ushort NextUShort();
ushort NextUShort(int maxValue);
ushort NextUShort(int minValue, int maxValue);
int NextInt();
int NextInt(int maxValue);
int NextInt(int minValue, int maxValue);
uint NextUInt();
uint NextUInt(uint maxValue);
uint NextUInt(uint minValue, uint maxValue);
long NextLong();
long NextLong(long maxValue);
long NextLong(long minValue, long maxValue);
ulong NextULong();
ulong NextULong(ulong maxValue);
ulong NextULong(ulong minValue, ulong maxValue);
BigInteger NextBigInteger(int BitLength = 128);
BigInteger NextBigUnsignedInteger(int BitLength = 128);
BigInteger NextBigInteger(int MinBitLength, int MaxBitLength);
BigInteger NextBigUnsignedInteger(int MinBitLength, int MaxBitLength);
float NextFloat(float minValue, float maxValue);
float NextFloat();
double NextDouble(double minValue, double maxValue);
double NextDouble();
void GetBytes(byte[] data);
byte GetNextByte();
char GetNextChar();
byte[] GetNextByteArray(int size);
char[] GetNextCharArray(int size);
```

## Extensions

Many useful method extensions
Example:

```C#
//Match
int a = 10;
int b = a.Match(
(x => x == 10, 20),
(x => x == 2, 30)
);

int c = a.Match(
(10, 20), (2, 30)
);
``` 

## Monadas

```
NBuilder
NEither
NNone
NOptional
NResult
NSwitcher
NTry
```
NOptional example

```C#
int result = NOptional<int>.OfNullable(10).Filter(x => x != 2).Map(x => x * 2).OrElse(0);
//Result == 20
NOptional<int>.OfNullable(20).Filter(x => x != 20).IfPresent(x => WriteLine(20));
//Nothing
```
NSwitcher example
```C#
private static NSwitcher<SudokuPuzzleLevel> sudokuPuzzleLevelSwitch = OfSwitcher(
            (SudokuLevel.UNKNOWN, SudokuPuzzleLevel.UNKNOWN),
            (SudokuLevel.IDIOTIC, SudokuPuzzleLevel.IDIOTIC),
            (SudokuLevel.VERY_SIMPLE, SudokuPuzzleLevel.VERY_SIMPLE),
            (SudokuLevel.SIMPLY, SudokuPuzzleLevel.SIMPLY),
            (SudokuLevel.MODERATE, SudokuPuzzleLevel.MODERATE),
            (SudokuLevel.NORMAL, SudokuPuzzleLevel.NORMAL),
            (SudokuLevel.HARD, SudokuPuzzleLevel.HARD),
            (SudokuLevel.VERY_HARD, SudokuPuzzleLevel.VERY_HARD),
            (SudokuLevel.EXTREMELY_HARD, SudokuPuzzleLevel.EXTREMELY_HARD)
        );
var result = sudokuPuzzleLevelSwitch
.Match(SudokuLevel.IDIOTIC)
.OrElse(SudokuPuzzleLevel.UNKNOWN)
//Result == SudokuPuzzleLevel.IDIOTIC
```

## License
[MIT](https://choosealicense.com/licenses/mit/)