import { trigger, state, style, transition, animate } from "@angular/animations";

export const fadeIn = trigger("fadeIn", [
  state("void", style({ opacity: 0 })),
  state("*", style({ opacity: 1 })),
  transition(":enter", animate("600ms ease-in")),
  transition(":leave", animate("600ms ease-in"))
]);
