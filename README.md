embedded database benchmark on dotnet core 2.1
===========================================

A simple benchmark based on [mbdavid's work](https://github.com/mbdavid/LiteDB-Perf), but running on dotnet core
with latest SQLite and LiteDB v4. Extra test cases such as in-memory-database and sqlite with dapper(an orm framework)
are added.

## platform
windows 10 1803, Intel Core i7-4790 3.6Ghz, 16G RAM, NO SSD

## result

```text
LiteDB: default - 5000 records
==============================
Insert         :  3617 ms -     1382 records/second
Bulk           :    80 ms -    61748 records/second
Update         :  2404 ms -     2080 records/second
CreateIndex    :   105 ms -    47359 records/second
Query          :    86 ms -    57936 records/second
Delete         :    31 ms -   159405 records/second
Drop           :    13 ms -   376163 records/second
FileLength     :  7208 kb

LiteDB: exclusive no journal - 5000 records
===========================================
Insert         :  1458 ms -     3427 records/second
Bulk           :    55 ms -    90241 records/second
Update         :  1225 ms -     4079 records/second
CreateIndex    :    61 ms -    81525 records/second
Query          :    28 ms -   175368 records/second
Delete         :    23 ms -   212792 records/second
Drop           :     8 ms -   595699 records/second
FileLength     :  7324 kb

SQLite: default - 5000 records
==============================
Insert         : 642165 ms -        8 records/second
Bulk           :   159 ms -    31401 records/second
Update         :   275 ms -    18169 records/second
CreateIndex    :    96 ms -    51755 records/second
Query          :   238 ms -    21000 records/second
Delete         :   104 ms -    47911 records/second
Drop           :    79 ms -    63268 records/second
FileLength     :  3600 kb

SQLite: no journal - 5000 records
=================================
Insert         : 144489 ms -       35 records/second
Bulk           :    59 ms -    84401 records/second
Update         :   284 ms -    17562 records/second
CreateIndex    :    26 ms -   189078 records/second
Query          :   241 ms -    20722 records/second
Delete         :    19 ms -   262407 records/second
Drop           :    16 ms -   302651 records/second
FileLength     :  3528 kb

SQLite in memory: default - 5000 records
========================================
Insert         :    28 ms -   177573 records/second
Bulk           :    16 ms -   310569 records/second
Update         :    12 ms -   391797 records/second
CreateIndex    :     2 ms -  1833651 records/second
Query          :    17 ms -   286192 records/second
Delete         :     0 ms -  8087997 records/second
Drop           :     0 ms - 10008006 records/second
FileLength     :     0 kb

SQLite in memory: no journal - 5000 records
===========================================
Insert         :    15 ms -   315948 records/second
Bulk           :    10 ms -   465988 records/second
Update         :    10 ms -   463594 records/second
CreateIndex    :     2 ms -  1701433 records/second
Query          :    13 ms -   372345 records/second
Delete         :     0 ms - 13793103 records/second
Drop           :     0 ms - 10815488 records/second
FileLength     :     0 kb

LiteDB in memory: default - 5000 records
========================================
Insert         :   265 ms -    18810 records/second
Bulk           :    34 ms -   144625 records/second
Update         :    64 ms -    77540 records/second
CreateIndex    :   104 ms -    47783 records/second
Query          :    34 ms -   146952 records/second
Delete         :    21 ms -   236254 records/second
Drop           :     6 ms -   785225 records/second
FileLength     :     0 kb

SQLite in memory using dapper: default - 5000 records
=====================================================
Insert         :   134 ms -    37041 records/second
Bulk           :    22 ms -   226042 records/second
Update         :    62 ms -    80021 records/second
CreateIndex    :     2 ms -  1773364 records/second
Query          :   126 ms -    39676 records/second
Delete         :     0 ms -  7477195 records/second
Drop           :     0 ms -  9980040 records/second
FileLength     :     0 kb

SQLite in memory using dapper: no journal - 5000 records
========================================================
Insert         :    58 ms -    85860 records/second
Bulk           :    16 ms -   303335 records/second
Update         :    62 ms -    80139 records/second
CreateIndex    :     2 ms -  2022081 records/second
Query          :    88 ms -    56718 records/second
Delete         :     0 ms - 15743073 records/second
Drop           :     0 ms - 14912019 records/second
FileLength     :     0 kb
```
