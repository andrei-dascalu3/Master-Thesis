# Thesis plan: TD-aware comparison of LLM migration approaches

> Based on: Olariu & Alboaie (2026), *A Skill-Mediated LLM Workflow for Migrating a .NET Monolith to Cloud-Deployed Microservices: a Controlled Case Study*, IEEE Access.
> Artifacts: https://github.com/florinolariuip/ieee-access-monolith-migration · Zenodo DOI 10.5281/zenodo.20422420

**Original proposal (RO):**
*Replicarea metodologiilor prezentate in articol si continuarea plus compararea acestora in perspectiva extinderii codebase-ului cu o noua functionalitate complexa (unul sau mai multe user flow-uri noi) si analiza statica, dar si dinamica, calitatativa in cele doua cazuri rezultate. Astfel, se poate estima technical debt-ul introdus de cele doua abordari.*

**Proposal (EN, adapted):**
*Re-running the two migration approaches from the paper (skill-mediated and fully-specified) on the PetRescue monolith, using a newer model (Claude Sonnet 5.5). Both resulting microservice systems are deployed (local Docker Compose as the baseline, Azure as a time-boxed extension). The same complex new feature (the adoption user flow) is then added to each codebase, again using each approach. The two resulting codebases are compared through static analysis (Sonar TD, coupling, change amplification) and dynamic analysis (hidden acceptance tests, coverage, load and performance tests). This estimates the technical debt and performance impact introduced by each approach.*

**Scope decisions:** the subject system is PetRescue only. The hexagonal arm, matched-specificity condition, Pact, chaos testing, NDepend and blind review are out of scope. A second feature is an optional stretch goal. Timeline: about 17 weeks.

---

## 1. Research questions

- **RQ1 – Replication on a newer model:** Do the paper's conclusions hold with Claude Sonnet 5.5? The paper (on `claude-sonnet-4-5`) reports skill-mediated 12.1/20 and fully-specified 14.8/20. Differences are interpreted as model drift, not replication failure. An optional pilot (N=2 per arm) on `claude-sonnet-4-5` separates model effects from setup effects, if the model is still available.
- **RQ2 – Evolvability:** When the same adoption flow is added to each codebase, which one needs less change amplification (files, services and lines touched)?
- **RQ3 – Technical debt:** How much ΔTD does each approach introduce, measured statically (Sonar TD, coupling, smells) and dynamically (hidden acceptance tests, coverage)?
- **RQ4 – Performance:** How do the two systems differ in p95 latency, throughput, call depth and resource use, locally (Docker Compose) and on Azure?

**Hypotheses** (exploratory, small N):
- **H1:** the skill-mediated arm has lower ΔTD.
- **H2:** the skill-mediated arm has lower change amplification.
- **H3:** the skill-mediated arm has lower p95 latency on the new flow.
- **H0:** there is no difference between the arms.

## 2. Phases

Total: about 17 weeks. Literature review runs in parallel with phases 1–2, and writing starts early.

| # | Phase | Output | Approx. duration |
|---|---|---|---|
| 0 | Literature review: TD (Cunningham, SQALE, Avgeriou), microservice smells (Taibi), LLM4SE | Chapter 2 | 2–3 weeks (in parallel) |
| 1 | **Replication** using the Zenodo/GitHub artifacts (`scoring/aggregate.py`, `experiments/dimensions.json`). Run the skill-mediated and fully-specified conditions with N=5 each on Claude Sonnet 5.5 (pin the exact model ID and date). Optional pilot on `claude-sonnet-4-5` | Replication report and comparison with the paper's means and SDs | 3 weeks |
| 2 | **Deployment**: Docker Compose locally (primary baseline), then Azure Container Apps for a time-boxed 1–2 week window (min replicas = 1 during measurements). Infrastructure as code for reproducibility | Deployed systems | 2 weeks |
| 3 | **Feature specification**: the adoption flow, e.g. *adoption application → screening → visit scheduling → adoption fee/donation → notifications*. Acceptance criteria in Gherkin, plus a hidden acceptance test suite | Feature spec and oracle tests | 1–2 weeks |
| 4 | **Extension**: apply the same agent protocol (skill-mediated vs fully-specified) to each resulting codebase, N=3–5 runs per arm. Log tokens, cost, time and retries. Optional stretch: second feature | Extended codebases and logs | 3 weeks |
| 5 | **Analysis**: static metrics, hidden tests, load tests locally and on Azure (see §3) | Dataset | 2 weeks |
| 6 | Statistics, threats to validity, writing | Thesis and reproducibility package | 4 weeks |

## 3. Metrics and tools

### Static analysis (measured before and after the feature, to compute Δ)
- **SonarQube:** TD minutes, TD ratio, code smells, cognitive complexity, duplication.
- **.NET code metrics:** maintainability index, coupling, and Martin's instability and abstractness.
- **ArchUnitNET (optional):** rules for layer and service boundaries.
- **Cross-service coupling:** for example, a shared database or chatty calls between services.
- **Change amplification:** the number of files, services and lines touched per feature.

### Dynamic analysis
- **Hidden acceptance tests:** pass rate.
- **Tests:** coverage. Mutation score with Stryker.NET only if time allows.
- **Load tests (k6):** p95 latency, throughput and error rate on the new flow, run on local Docker and on Azure. Repeat each test several times and report medians.
- **Tracing:** OpenTelemetry traces, measuring call depth (network hops) and attributing latency to services.
- **Resources:** CPU and RAM per service, cold start time, and cost per 1,000 requests on Azure.

### TD estimate
- ΔTD normalized by feature size (per KLOC added or per user flow), combined with remediation cost.

## 4. Deployment strategy
- **Primary baseline:** local Docker Compose on a personal machine with WSL. All core results must not depend on cloud credits.
- **Azure (extension):** a free account ($200 for 30 days, credit card required) with Azure Container Apps. Start the credit only when both extended systems are ready, and run all load tests in one or two weeks. Set min replicas to 1 for measurements. Fallback: a single VM running Docker Compose.
- **Caveat:** free tiers have throttled CPU and noisy latency, so report cloud numbers next to local ones and interpret them with care.
- Document the deployment as IaC (Docker Compose, Bicep or Terraform).

## 5. Statistics and methodology
1. Use non-parametric statistics: Mann–Whitney U with Cliff's δ. With N=3–5 per arm, treat results as exploratory and focus on effect sizes more than p-values.
2. Write your own dynamic oracle instead of relying only on the paper's rubric.
3. Report cost, tokens and time as an efficiency dimension, using the exact usage that Claude Code reports per run.
4. Cover threats to validity following Runeson & Höst: LLM non-determinism, model drift (different model from the paper), a single subject system, small N, and noisy free-tier cloud.
5. Publish a reproducibility package containing the scripts, prompts, IaC and raw data.
6. Stretch goal: a second feature to show whether TD accumulates.

## 6. Thesis structure (about 70–90 pages)
Check the faculty template first, because some programs require a fixed layout.

1. **Introduction** (4–5 p.): motivation, problem, RQ1–RQ4 and hypotheses, contributions, outline.
2. **Background and related work** (12–15 p.): technical debt (Cunningham, SQALE, Avgeriou) and its measurement; monolith-to-microservices migration and microservice smells (Taibi); LLMs and agents in software engineering and the maintainability of AI-generated code; the Olariu & Alboaie paper and the gap this thesis fills.
3. **Research methodology** (8–10 p.): case-study design (Runeson & Höst), the two arms, PetRescue, model and tooling (Claude Code, Sonnet 5.5), run counts, protocol.
4. **Replication of the migration** (8–10 p.): setup, deviations from the paper, rubric results against the paper's means (RQ1), model drift.
5. **Deployment and environment** (5–7 p.): architecture of the two resulting systems, Docker Compose, Azure deployment, IaC.
6. **Feature extension** (8–10 p.): adoption flow specification, Gherkin criteria, hidden oracle tests, extension protocol, change amplification (RQ2).
7. **Technical debt and performance analysis** (10–12 p.): static metrics (Sonar, coupling, smells), dynamic metrics (tests, coverage, k6, OpenTelemetry, resources), cost and token efficiency, statistics (RQ3, RQ4).
8. **Discussion** (5–6 p.): answers to the RQs, hypothesis outcomes, implications, skill-mediated workflow vs raw prompt trade-off.
9. **Threats to validity** (3–4 p.): internal, external, construct and conclusion validity (LLM non-determinism, model drift, small N, single subject system, noisy free-tier cloud).
10. **Conclusions and future work** (3–4 p.): contributions, limits, future work (second feature, hexagonal arm, other systems).

**Back matter:** references and appendices (prompts and skills, rubric, Gherkin scenarios, raw metric tables, reproducibility package).

**Writing order:** methodology first, related work in parallel with the experiments, introduction and conclusions last. If pages run short, merge chapters 4 and 5, or 6 and 7.

## 7. Risks
- **Timeline:** about 17 weeks is tight, so parallelise literature and replication, and start writing early.
- **Budget:** about 16–20 runs. The paper observed about $1.50 per `/refactor` run (harness cap $5–8), so the estimate is about $30–60. Budget $50–100 including pilots and a safety margin.
- **Tooling and billing:** use Claude Code (the paper's harness and skills are built for it) with Anthropic API pay-as-you-go billing, for exact per-run token and cost logs. A Claude Max subscription is an alternative, but rolling usage limits can stall batches and cost per run is only estimated. Copilot is not used for the experiments. Verify that Claude Sonnet 5.5 works with the harness `--model` flag, and run 2–3 pilot runs to measure real cost.
- **Model deprecation or drift:** pin the exact model ID and date, and keep all runs on one model.
- **Cloud credits:** no student email, so rely on the Azure free account and a short measurement window.
- **Windows environment:** the bash scripts need WSL or Git Bash.
- **Scope creep:** keep the second feature optional, and do not add other arms or subject systems.
