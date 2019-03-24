import { Component, OnInit } from "@angular/core";
import { fadeInOut } from "./shared/animations";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
  animations: [fadeInOut]
})
export class AppComponent implements OnInit {
  title = "pho84-snack-web";

  constructor() {}

  ngOnInit(): void {}

  getAnimationState(outlet: RouterOutlet) {
    return outlet.activatedRouteData.animation;
  }
}
