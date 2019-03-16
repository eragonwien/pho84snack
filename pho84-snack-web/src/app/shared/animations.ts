import { trigger, state, style, transition, animate, query } from "@angular/animations";

export const fadeInOut = trigger("fadeInOut", [
  state("void", style({ opacity: 0 })),
  state("*", style({ opacity: 1 })),
  transition(":enter", animate("600ms ease-in")),
  transition(":leave", animate("600ms ease-in"))
]);

export const collapseAnimation = trigger("collapseAnimation", [
  state("open", style({ height: 100 })),
  state("close", style({ height: 0 })),
  transition("* <=> *", animate("600ms ease-in"))
]);
