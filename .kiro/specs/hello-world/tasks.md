# Implementation Plan: Hello World

## Overview

Implement a ColorButton component in C# that cycles through a predefined list of colors on each click, maintaining local state and updating the visual display accordingly.

## Tasks

- [x] 1. Set up project structure and core types
  - Create `ComponentState` record/class with `Colors` (string array) and `CurrentIndex` (int) properties
  - Define validation rules: non-empty colors list, index in bounds
  - _Requirements: 1.2, 3.1_

- [x] 2. Implement core state logic
  - [x] 2.1 Implement `HandleClick` method
    - Advance `CurrentIndex` to `(CurrentIndex + 1) % Colors.Length`
    - Return updated `ComponentState` with colors array unchanged
    - _Requirements: 2.1, 2.2, 2.4_

  - [ ]* 2.2 Write property test for cycle round-trip
    - **Property 1: Cycle Round-Trip**
    - **Validates: Requirements 1.1, 3.2**

  - [ ]* 2.3 Write property test for index always in bounds
    - **Property 2: Index Always In Bounds**
    - **Validates: Requirements 2.1, 3.1**

  - [ ]* 2.4 Write property test for colors array immutability
    - **Property 3: Colors Array Immutability**
    - **Validates: Requirements 2.4**

  - [x] 2.5 Implement `GetCurrentColor` method
    - Return `Colors[CurrentIndex]`
    - _Requirements: 1.1, 2.2_

- [x] 3. Implement error handling
  - [x] 3.1 Handle empty color list on initialization
    - If `Colors` is null or empty, default to `["gray"]` and set `CurrentIndex` to 0
    - Expose an `IsInteractive` flag (false when using fallback)
    - _Requirements: 4.1, 4.2_

  - [x] 3.2 Handle out-of-bounds index recovery
    - If `CurrentIndex < 0` or `CurrentIndex >= Colors.Length`, reset to 0
    - _Requirements: 5.1, 5.2_

  - [ ]* 3.3 Write property test for out-of-bounds index reset
    - **Property 4: Out-of-Bounds Index Reset**
    - **Validates: Requirements 5.1**

- [x] 4. Checkpoint - Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

- [x] 5. Implement the ColorButton UI component
  - [x] 5.1 Create `ColorButton` component wiring state logic to the UI
    - Render a button element with background color set to `GetCurrentColor(state)`
    - On click, call `HandleClick` and update local state
    - Disable click interaction when `IsInteractive` is false
    - _Requirements: 1.1, 2.2, 2.3, 4.2_

  - [ ]* 5.2 Write unit tests for ColorButton rendering
    - Verify initial render shows color at index 0
    - Verify each click advances to the next color
    - Verify wrap-around after the last color
    - Verify disabled state when initialized with empty list
    - _Requirements: 1.1, 2.3, 4.1, 4.2_

  - [ ]* 5.3 Write integration tests for click sequence
    - Simulate a sequence of user clicks and assert rendered color matches expected values at each step
    - _Requirements: 2.1, 2.2, 2.3, 3.2_

- [x] 6. Final checkpoint - Ensure all tests pass
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster MVP
- Each task references specific requirements for traceability
- Property tests use fast-check (or a C# equivalent such as FsCheck)
- Checkpoints ensure incremental validation
