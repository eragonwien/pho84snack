import { trigger, state, style, transition, animate, query, group } from "@angular/animations";

export const fadeInOut = trigger("fadeInOut", [
  transition("*<=>*", [style({ opacity: 0 }), animate("0.5s ease-in", style({ opacity: 1 }))])
]);

export const slideInLeft = trigger("slideInLeft", [
  state("show", style({ opacity: 1, transform: "translateX(0)" })),
  state("hide", style({ opacity: 0, transform: "translateX(-100%)" })),
  transition("hide=>show", animate("1s ease")),
  transition("show=>hide", animate("1s ease"))
]);

export const slideInRight = trigger("slideInRight", [
  state("show", style({ opacity: 1, transform: "translateX(0)" })),
  state("hide", style({ opacity: 0, transform: "translateX(100%)" })),
  transition("hide=>show", animate("1s ease")),
  transition("show=>hide", animate("1s ease"))
]);
