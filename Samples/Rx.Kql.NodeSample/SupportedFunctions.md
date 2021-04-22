# Supported Functions

This document provides the supported scalar and tabular functions, as well as comparison operators available to realtime KQL Queries.

## Supported Scalar Functions

For more details, see [Scalar Functions](https://kusto.azurewebsites.net/docs/query/scalarfunctions.html).

```text
1. ago
2. bag_unpack
3. between
4. bin
5. case
6. cpu_usage
7. datepart
8. datetime
9. extract
10. format_datetime
11. hash_sha256
12. insubnet
13. ipaddressinrange
14. ipv4_fromnumber
15. isempty
16. isnotempty
17. isnotnull
18. isnull
19. not
20. notbetween
21. now
22. pack
23. packarray
24. parse_ipv4
25. replace
26. reverse
27. split
28. strcat
29. strlen
30. substring
31. throw
32. time
33. tobool
34. todatetime
35. tolower
36. tostring
37. totimespan
38. toupper
39. trim
40. trim_end
41. trim_start
```

## Supported Tabular Functions

For more information, see [Tabular Operators](https://kusto.azurewebsites.net/docs/query/queries.html).

```text
1. evaluate (for bag_unpack)
2. extend
3. project
4. where
5. limit
6. summarize
    1. count()
    2. sum()
    3. makelist()
    4. makeset()
    5. min()
    6. max()
```

## Suported Comparison Operators

For more information, see [Logical](https://kusto.azurewebsites.net/docs/query/logicaloperators.html) or [String](https://kusto.azurewebsites.net/docs/query/datatypes-string-operators.html).

``` text
1. = (project, extend, static values...)
2. ==
3. =~
4. !~
5. <
6. >
7. <=
8. >=
9. contains
10. !contains
11. has
12. !has
13. contains_cs
14. !contains_cs
15. startswith
16. !startswith
17. startswith_cs
18. !startswith_cs
19. endswith
20. !endswith
21. endswith_cs
22. !endswith_cs
23. or
24. and
25. in
26. !in
27. not
28. between
29. !between
30. matches regex
```
