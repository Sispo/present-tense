# Afirmative Sentences Generator

### Related projects
[Pushdown automaton implementation in C#](https://github.com/tymofiidolenko/pushdown-automaton)

[Context-free grammar to pushdown automaton converter](https://github.com/tymofiidolenko/grammarton)

### Grammar preview
#### CFG (LL)

* S → SB or B or DMPF B or B DMPF
* SB → SBWPP or SBWPS
* B → BS or BP
* BS → SBWPS VS or SBADVMVS or SBADVFVS
* BP → SBWPP VP or SBADVMVP or SBADVFVP
* [More...]()

The app generates such sentences as:

>The informal brushes.
>
>Often he apologizes.
>
>The alternative card.
>
>She cries.
>
>She.
>
>He regularly washes.
>
>I never inject.
>
>You vanish unfortunately enough.
>
>The lunch occasionally flaps.
>
>Mckenzie quickly cheats.

It is build on [context-free grammar](https://en.wikipedia.org/wiki/Context-free_grammar/) using [pushdown automaton](https://en.wikipedia.org/wiki/Pushdown_automaton).
