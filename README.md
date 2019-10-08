# NQuadratic
A translation service to convert from standard form, `ax²+bx+c`, to the factored, `d(ex+h)(fx+g)`, and vertex, `a(x-x₀)²+y₀`, forms.

## Console Usage
```
dotnet run a=... b=... c=...
```

## Math
### Factored to Standard
```
d(ex+h)(fx+g)
  = d(efx² + (eg+fh)x + gh)

a = d * ef
b = d * (eg + fh)
c = d * gh

d = gcd(a, b, c)
a' = a / d = ef
b' = b / d = eg + fh
c' = c / d = gh 
```
To perform the translation from `a`, `b`, & `c` requires factoring to get to necessary values of `e`, `f`, `g`, & `h`.

### Standard to Factored with `c = 0`
```
ax²+bx

d = gcd(a, b)
a' = a / d
b' = b / d

d(a'x² + b'x)
  = dx(a'x + b')
  = d(x + 0)(a'x + b')

e = 1
h = 0
f = a'
g = b'
```

### Vertex to/from Standard
```
a(x-x₀)²+y₀
  = a(x² - 2x₀x + x₀²) + y₀
  = ax² - 2ax₀x + ax₀² + y₀

a = a
b = -2ax₀
c = ax₀² + y₀

x₀ = -b / 2a
y₀ = c - ax₀²
   = c - a(-b / 2a)²
   = c - b² / 4a
```
