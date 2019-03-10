import { Component, OnInit, Input } from "@angular/core";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-hero",
  templateUrl: "./hero.component.html",
  styleUrls: ["./hero.component.css"]
})
export class HeroComponent implements OnInit {
  @Input() image: string;
  @Input() title: string;
  @Input() subtitle: string;
  @Input() size: string;
  @Input() logo: string;
  background: string;
  heroClass: string;

  title1: string;
  title2: string;

  constructor(public hs: HelperService) {}

  ngOnInit() {
    this.splitTitle();
    this.image = this.hs.imagePath(this.image);
    this.logo = this.logo ? this.hs.imagePath(this.logo) : "";
    this.background = this.hs.imageUrlPath(this.image);
    this.heroClass = "hero primary " + (this.size ? this.size : "is-large");
  }

  splitTitle() {
    if (this.title && this.title.includes("-")) {
      let index = this.title.indexOf("-");
      this.title1 = this.title.substring(0, index);
      this.title2 = this.title.substring(index + 1);
    }
  }
}
