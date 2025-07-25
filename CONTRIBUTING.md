# Contributing to UniFlux

Thank you for your interest in contributing to UniFlux! This document provides guidelines and information for contributors.

**Working on your first Pull Request?** You can learn how from this *free* series [How to Contribute to an Open Source Project on GitHub](https://kcd.im/pull-request)

## Legal Requirements

All contributions are subject to the [Unity Contribution Agreement (UCA)](https://unity3d.com/legal/licenses/Unity_Contribution_Agreement).
By making a pull request, you are confirming agreement to the terms and conditions of the UCA, including that your Contributions are your original creation and that you have complete right and authority to make your Contributions.

## Development Setup

### Prerequisites
- Unity 2019.3 or later
- .NET compatible IDE (Visual Studio, Rider, VS Code)
- Git

### Getting Started
1. Fork and clone the repository
2. Open the project in Unity or your preferred IDE
3. Run existing tests to ensure everything works: `Tests/EditMode/`

## Coding Standards

### Code Style
- Use 4 spaces for indentation (configured in .editorconfig)
- Follow C# naming conventions
- Add XML documentation for public APIs
- Keep methods focused and small
- Use `readonly` for fields that don't change after construction

### Performance Guidelines
- Avoid unnecessary allocations in hot paths
- Use `EqualityComparer<T>.Default` instead of `object.Equals()` for value types
- Prefer direct enumeration over foreach when performance is critical
- Cache expensive operations when possible

### Documentation
- Add XML documentation comments for all public APIs
- Include code examples in documentation when helpful
- Update README.md if adding new features
- Document any breaking changes in CHANGELOG.md

## Testing

### Test Requirements
- Add unit tests for all new functionality
- Ensure tests are deterministic and don't depend on external resources
- Use descriptive test names that explain what is being tested
- Include both positive and negative test cases

### Running Tests
Tests are located in `Tests/EditMode/` and use NUnit framework. Run them through Unity's Test Runner or compatible test runner.

## Pull Request Process

### Before Submitting
1. Ensure all tests pass
2. Add tests for new functionality
3. Update documentation as needed
4. Follow the coding standards above
5. Keep commits focused and atomic

### PR Guidelines
- Provide a clear description of changes
- Reference any related issues
- Include test results if applicable
- Be responsive to feedback and reviews

### Performance Impact
If your changes affect performance:
- Include benchmark results
- Explain the performance implications
- Consider backward compatibility

## Issue Reporting

When reporting issues:
- Use the issue template if available
- Provide clear reproduction steps
- Include Unity version and platform information
- Add relevant error messages or logs

## Architecture Considerations

UniFlux follows the Flux pattern:
- **Actions**: Represent events or commands
- **Dispatcher**: Central hub for dispatching actions
- **Stores**: Hold application state and logic
- **Views**: React to state changes

When contributing:
- Maintain unidirectional data flow
- Keep state mutations in stores
- Avoid direct state access outside of stores
- Consider performance implications of state changes

## License

By contributing to UniFlux, you agree that your contributions will be licensed under the MIT License.