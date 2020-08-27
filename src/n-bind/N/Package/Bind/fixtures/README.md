# fixtures

Components cannot be used if they are in the `Editor` folder, but tests must be.

This components and files are all wrapped in `#if N_BIND_TESTS` so they should
not interfere with other things.
