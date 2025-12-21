# Security Policy

## Supported Versions

We are currently supporting the following versions with security updates:

| Version | Supported          |
| ------- | ------------------ |
| 2.1.x   | :white_check_mark: |
| 2.0.x   | :white_check_mark: |
| < 2.0   | :x:                |

## Reporting a Vulnerability

We take the security of UniFlux seriously. If you have discovered a security vulnerability, we appreciate your help in disclosing it to us in a responsible manner.

### How to Report

Please report security vulnerabilities by sending an email to **arpaxavier@gmail.com** with the following information:

- Type of issue (e.g., buffer overflow, SQL injection, cross-site scripting, etc.)
- Full paths of source file(s) related to the manifestation of the issue
- The location of the affected source code (tag/branch/commit or direct URL)
- Any special configuration required to reproduce the issue
- Step-by-step instructions to reproduce the issue
- Proof-of-concept or exploit code (if possible)
- Impact of the issue, including how an attacker might exploit the issue

This information will help us triage your report more quickly.

### What to Expect

- You will receive a response from us within 48 hours acknowledging your report
- We will investigate the issue and determine its impact and severity
- We will work on a fix and coordinate a release timeline with you
- Once the issue is resolved, we will publicly acknowledge your responsible disclosure (unless you prefer to remain anonymous)

### What NOT to Do

Please do not:

- Open a public issue on GitHub for security vulnerabilities
- Attempt to exploit the vulnerability beyond what is necessary to demonstrate it
- Access, modify, or delete data that does not belong to you
- Disclose the vulnerability publicly until we have had a chance to address it

## Security Best Practices

When using UniFlux in your projects:

- Always use the latest stable version
- Keep Unity and all dependencies up to date
- Review the [CHANGELOG](CHANGELOG.md) for security-related updates
- Follow Unity's security best practices
- Validate and sanitize all user inputs
- Use secure communication protocols when dispatching sensitive data

## Security Updates

Security updates will be released as patch versions and documented in the [CHANGELOG](CHANGELOG.md). Subscribe to releases on GitHub to stay informed about security updates.

Thank you for helping keep UniFlux and its users safe!
