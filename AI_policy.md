AI Usage and Contribution Policy

This project has strict rules on what AI can and cannot be used for during development and contribution. (AI is defined as any tool resembling or including, but not limited to, Copilot, ChatGPT, Claude, Gemeni, or other LLMs).
The purpose of this policy is two-fold: to aid in the comprehension and understanding of both our own code base and the licenses that govern its parts, and to maintain the originality, accountability, and compliance of the software we produce.

1. Permitted Use Cases
  - Code Comprehension: Providing peer-to-peer or self-used documentation of existing code within this project or between contributors of this project through the process of generating overviews, explanations, or architectural breakdowns of what already exists, without adding the generated content to the repository, is allowed.
  - Debugging: Identifying Bugs, syntax errors, and performance bottlenecks within existing code is allowed, so long as the user does not prompt for or use solutions. Any such use must be noted
      - Sub Note: the user can not under any circumstance prompt for, or use any proposed changes from the AI during this process; all changes and ultimate solutions must come from the user, only the simple identification of what is and where is allowed.
  - Personal Understanding: In the processes of learning the system's tools and applications used across this project, the user is allowed to prompt an AI for exemplary code to understand proper syntax and workflow, so long as the code generated never touches the repository. Any such usage must be noted
  - Grammar: Spelling is a matter of English, not of programmers. The use of AI tools that explicitly aid in the grammatical composition, sentence flow, and spelling of written content(not code or comments in code) is allowed and must be noted.

Once the user has completed their approved AI usage, they must document it both in code and in their commit comment, following doxygen notation with an "@ AI: " section containing further details in the document ahead. Additionally, all subsequent implementations, additions, fixes, updates, or modifications in any respect must be directly created by you, the contributor, without the influence of AI.
Overall, the use of AI to directly generate, autocomplete, or write new code blocks, even if unlicensed, is strictly forbidden.

2. Mandatory Documentation.
Every instance of AI usage for any of the above-allowed reasons must be documented. You must include a doxygen-style comment immediately above the code with a section following the flow of "@ AI: " where the following items must be included.
  - the specific tool used (e.g., Copoliot, ChatGPT, Claude, Gemini, local Ollama model (gemma4:31b), etc.)
  - the specific prompt and repsonse used (best to if your text editor supports this wrapp it in {} so it can be colapsed).
    - any files used in the processes of generation
  - the reason for the generation rather than a standard internet search and research dive.
  - When creating a push request, the message must contain an AI at the end of your message to flag maintainers and administrators to understand the scope of your AI usage.

3. Third-Party License and Legal Responsibility
Contributors are required to maintain all relevant licenses to the letter; they must be maintained for all third-party tools, libraries, and/or applications integrated into or documented for use in this project.
  - You, the contributor, are responsible for ensuring that no AI tool does not unintentionally or intentionally add content, logic changes, or other affecting data that violates copyright-protected third-party licenses or even non-restrictive licenses and adhere to their individual rules laid out for usage and contribution.
  - Liability: If any undocumented or non-cp,[pliant AI-generated code is intigrated into the repositiry that incursed legal consiquences the idneviduak who riginally pushed that code()as determined through git push history) Bear full accountability for any resulting licensing conflicts, security vulnerabilities, or legal action incurred.
