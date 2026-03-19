# Implementation Plan: Second Button (RandomizeButton)

## Overview

Add a `Randomize` method to `ComponentState.cs`, mirror it in the JavaScript state module in `index.html`, then render a heart-shaped RandomizeButton that shares state with the existing ColorButton.

## Tasks

- [x] 1. Add `Randomize` method to `ComponentState.cs`
  - Add a `Randomize(Random rng)` method that returns a new `ComponentState` with `CurrentIndex` set to `rng.Next(Colors.Length)` and `Colors`/`IsInteractive` unchanged
  - _Requirements: 2.1, 2.3, 3.1, 4.1, 4.2_

  - [ ]* 1.1 Write property test: Randomize index always in bounds
    - **Property 1: Randomize index always in bounds**
    - For any non-empty color list of length N, `state.Randomize(rng).CurrentIndex` must satisfy `0 ≤ index < N`
    - Use `[Property(MaxTest = 100)]` and tag `Feature: second-button, Property 1: Randomize index always in bounds`
    - **Validates: Requirements 2.1, 3.1, 4.2**

  - [ ]* 1.2 Write property test: Colors array immutability after randomize
    - **Property 2: Colors array immutability after randomize**
    - For any `ComponentState`, `state.Randomize(rng).Colors` must be reference-equal and value-equal to `state.Colors`
    - Use `[Property(MaxTest = 100)]` and tag `Feature: second-button, Property 2: Colors array immutability after randomize`
    - **Validates: Requirements 2.3**

  - [ ]* 1.3 Write property test: Cycling continues correctly after randomize
    - **Property 3: Cycling continues correctly after randomize**
    - After `Randomize` yields index R, calling `HandleClick` k times must produce `currentIndex = (R + k) % colors.Length`
    - Use `[Property(MaxTest = 100)]` and tag `Feature: second-button, Property 3: Cycling continues correctly after randomize`
    - **Validates: Requirements 3.2, 5.1**

  - [ ]* 1.4 Write property test: Uniform distribution of randomized index
    - **Property 4: Uniform distribution of randomized index**
    - Over ≥ 1000 `Randomize` calls on a list of length N, each index appears with frequency ≈ 1/N (chi-squared test at p > 0.01)
    - Use `[Property(MaxTest = 1000)]` and tag `Feature: second-button, Property 4: Uniform distribution of randomized index`
    - **Validates: Requirements 4.1**

- [x] 2. Checkpoint — Ensure all C# tests pass
  - Ensure all tests pass, ask the user if questions arise.

- [x] 3. Add `randomize` function to JavaScript state mirror in `index.html`
  - Add `function randomize(state)` that returns `{ ...state, currentIndex: Math.floor(Math.random() * state.colors.length) }`
  - Place it alongside the existing `handleClick` and `getCurrentColor` functions
  - _Requirements: 2.1, 2.3, 3.1_

- [x] 4. Render the heart-shaped RandomizeButton in `index.html`
  - Add a `<button id="randomize-btn"></button>` element next to `#color-btn` in the `<body>`
  - Add CSS for `#randomize-btn` using `clip-path: path('M 40,70 C 10,50 0,30 0,20 A 20,20,0,0,1,40,10 A 20,20,0,0,1,80,20 C 80,30 70,50 40,70 Z')`, `width: 80px`, `height: 80px`, `background-color: hotpink`, and disabled/transition styles
  - _Requirements: 1.1, 6.1, 6.2, 6.3_

  - [ ]* 4.1 Write property test: Disabled state mirrors non-interactive ComponentState
    - **Property 5: Disabled state mirrors non-interactive ComponentState**
    - When `isInteractive` is false, the RandomizeButton must have `disabled` attribute set and clicks must not change `currentIndex`
    - Use `[Property(MaxTest = 100)]` and tag `Feature: second-button, Property 5: Disabled state mirrors non-interactive ComponentState`
    - **Validates: Requirements 1.2**

- [x] 5. Wire RandomizeButton click handler and update `render` in `index.html`
  - Add a click listener on `#randomize-btn` that calls `randomize(state)`, assigns the result to `state`, and calls `render()`
  - Update `render()` to also set `randomizeBtn.disabled = !state.isInteractive`
  - _Requirements: 1.2, 2.2, 3.2, 5.1, 5.2_

- [x] 6. Final checkpoint — Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for a faster MVP
- Property tests live in `tests/HelloWorld.Tests/ComponentStateTests.cs` alongside existing tests
- All implementation is confined to `src/HelloWorld.Core/ComponentState.cs` and `src/HelloWorld.Web/wwwroot/index.html`
