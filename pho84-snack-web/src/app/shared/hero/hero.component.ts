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
  background: string;
  heroClass: string;

  constructor(private hs: HelperService) {}

  ngOnInit() {
    this.image = this.hs.imagePath(this.image);
    this.background = this.hs.imageUrlPath(this.image);
    this.heroClass = "hero primary " + (this.size ? this.size : "is-large");
  }
}
