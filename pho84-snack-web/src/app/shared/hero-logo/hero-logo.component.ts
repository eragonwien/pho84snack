import { Component, OnInit } from "@angular/core";
import { HeroComponent } from "../hero/hero.component";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-hero-logo",
  templateUrl: "./hero-logo.component.html",
  styleUrls: ["./hero-logo.component.css"]
})
export class HeroLogoComponent extends HeroComponent {
  constructor(public hs: HelperService) {
    super(hs);
  }
}
