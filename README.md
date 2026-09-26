# Pactra

> A service engagement management platform for connecting clients with service providers and managing the complete engagement lifecycle.

**Pactra** is a portfolio project inspired by my experience with freelance creative work. It explores how a platform could centralize the workflow between a client and a service provider—from the initial service request and negotiation to agreement, delivery, completion, payment, and settlement.

Rather than building another simple CRUD application, Pactra is designed around **business workflows, state transitions, authorization, and data integrity**.

---

## Why Pactra?

As an artist who has experienced freelance work, I became familiar with the parts of a project that happen outside the actual creative work:

- Discussing what the client needs
- Negotiating pricing and scope
- Agreeing on deliverables
- Managing requirements and documents
- Providing progress updates
- Handling revisions
- Getting approval for completed work
- Tracking payments

These interactions can easily become scattered across messaging apps, email, file storage, and spreadsheets.

Pactra explores what it would look like to bring these interactions into a single structured workflow.

This is a **personal portfolio project**, not a production marketplace or commercially validated product. The goal is to use a realistic domain to practice designing and implementing a non-trivial application.

---

## Core Workflow

The central concept in Pactra is the **Engagement**.

```text
Client
   │
   ▼
Discover Service
   │
   ▼
Request Engagement
   │
   ▼
Negotiate Terms
   │
   ▼
Delivery Plan
   │
   ▼
Agreement
   │
   ▼
Sign + Deposit
   │
   ▼
Active Engagement
   │
   ├── Requirements
   ├── Submissions
   ├── Progress Updates
   └── Deliverables
   │
   ▼
Completion Review
   │
   ▼
Completed
   │
   ▼
Final Payment
   │
   ▼
Settled
```

The backend models this workflow as a controlled state machine rather than allowing users to arbitrarily change an engagement's status.

---

## User Roles

Pactra currently models four primary roles.

### Client

The client requests and pays for services.

Responsibilities include:

- Browse services
- Request engagements
- Provide project requirements
- Negotiate terms
- Sign agreements
- Submit required information
- Make payments
- Review completed work
- Request cancellation

### Provider

The provider offers services and performs the work.

Responsibilities include:

- Create and manage services
- Review engagement requests
- Negotiate terms
- Create delivery plans
- Sign agreements
- Create requirements
- Provide progress updates
- Upload deliverables
- Submit work for completion review

### Operations

Operations handles platform workflows that require verification or review.

Responsibilities include:

- Review requirement submissions
- Verify payments
- Review cancellation requests
- Handle defined engagement exceptions

Operations does not own the engagement itself.

### Platform Admin

Platform Admin manages the platform rather than individual engagements.

Responsibilities include:

- Manage users
- Manage roles
- Manage platform configuration
- Review audit logs

---

## Engagement Lifecycle

The primary engagement lifecycle is:

```text
REQUESTED
    │
    ▼
NEGOTIATING
    │
    ▼
AGREEMENT_PENDING
    │
    ▼
READY_TO_START
    │
    ▼
ACTIVE
    │
    ▼
COMPLETION_REVIEW
    │
    ▼
COMPLETED
    │
    ▼
SETTLED
```

Additional paths handle exceptional situations:

```text
REQUESTED
    └──► DECLINED
```

```text
AGREEMENT_PENDING
    └──► EXPIRED
```

```text
ACTIVE
    │
    ▼
CANCELLATION_REQUESTED
    ├──► CANCELLED
    └──► ACTIVE
```

The application controls which actions are valid for each state.

---

## Key Features

### Service Discovery

Providers can create services describing what they offer, while clients can discover available services.

### Engagement Requests

Clients can create an engagement containing information such as:

- Description
- Goals
- Requested features
- Constraints
- Budget
- Desired start date
- Additional information

### Negotiation

Multiple proposals can be created for an engagement while preserving the negotiation history.

For example:

```text
Client     → ₱50,000
Provider   → ₱60,000
Client     → ₱55,000
Provider   → Accepts
```

The accepted proposal becomes part of the finalized engagement terms.

### Delivery Planning

Providers can describe how they intend to deliver the requested service, including:

- Scope
- Deliverables
- Milestones
- Timeline
- Assumptions
- Exclusions

### Agreements

Once terms are finalized, an agreement is created.

The MVP models signatures separately for the client and provider and requires both signatures before the engagement can proceed.

### Payment Verification

The MVP uses a manual payment verification workflow rather than integrating a real payment gateway.

```text
Payment Required
       │
       ▼
Client Submits
       │
       ▼
Operations Reviews
       │
   ┌───┴────┐
   ▼        ▼
Verified  Rejected
```

Rejected payment attempts remain in the database instead of being overwritten.

### Requirements

Providers can create requirements during an active engagement.

Clients can submit documents or other materials to satisfy those requirements.

Both requirements and their submissions maintain their own statuses so the system can distinguish:

```text
Requirement
= What needs to be provided?

Submission
= What did the client provide?
```

### Completion Review

Providers submit completed work for review.

The client can either accept the completion or request changes.

```text
ACTIVE
   │
   ▼
COMPLETION_REVIEW
   ├── Accept → COMPLETED
   └── Changes → ACTIVE
```

### Activity & History

Pactra separates current state from historical information.

For example:

```text
Engagement
    = Current state

EngagementStatusHistory
    = Lifecycle transitions

EngagementActivity
    = Events that happened

AuditLog
    = Important system actions
```

This allows the system to preserve an understandable history of an engagement.

---

## Tech Stack

### Backend

- **C#**
- **ASP.NET Core Web API**
- **Entity Framework Core**

### Database

- **PostgreSQL**

### Frontend

- **Angular**

### Authentication

- **JWT**
- **Refresh Tokens**

### Infrastructure

- **Docker**

### API Documentation

- **Swagger / OpenAPI**

### Testing

- **xUnit**

---

## Architecture

The backend follows a layered approach:

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
Application / Service Layer
     │
     ▼
Domain Rules
     │
     ▼
Entity Framework Core
     │
     ▼
PostgreSQL
```

The goal is to keep responsibilities separated so that controllers primarily handle HTTP concerns while business rules remain in the appropriate application/domain layers.

---

## Database Design

The `Engagement` is the central domain entity.

Major entities include:

```text
User
Role
UserRole
Session

Service
Engagement

Proposal
DeliveryPlan

Agreement
AgreementSignature

Payment

Requirement
RequirementSubmission

CancellationRequest

EngagementStatusHistory
EngagementActivity

AuditLog
```

The database also enforces important invariants where appropriate.

Examples include:

```text
Engagement.provider_id
        =
Service.provider_id
```

and:

```text
UNIQUE (agreement_id, signer_role)
```

as well as partial uniqueness rules for active payments and pending cancellation requests.

The intention is to avoid relying entirely on application-level validation when a rule must remain true at the database level.

---

## Project Status

**In Development**

The project is being developed incrementally.

Current focus:

- Domain modeling
- Database design
- Entity Framework Core configuration
- Authentication and authorization
- Engagement state transitions
- Core REST API
- Automated testing

Planned work will expand into the Angular client and additional platform workflows.

---

## Project Scope

Pactra intentionally keeps several advanced features outside the MVP.

Not currently included:

- Real payment gateway integration
- Real-time chat
- Advanced project management
- Gantt charts
- Kanban boards
- Time tracking
- Ratings and reviews
- Subscriptions
- Referral systems
- Complex dispute resolution
- Commission systems

These features may be considered later if they support the project's learning goals.

---

## Disclaimer

Pactra is an independent portfolio project created for learning and demonstrating software engineering skills.

The product concept, workflows, and business rules are designed for the project and are not intended to represent a production-ready marketplace or legally binding service platform.

---

## Author

**Mikee Laurente**

Full-Stack Developer
Philippines

[GitHub](https://github.com/mikeelaurente) · [LinkedIn](https://www.linkedin.com/in/mikee-laurente-0773313a6)

---

## License

All rights reserved — for portfolio/demonstration purposes only
