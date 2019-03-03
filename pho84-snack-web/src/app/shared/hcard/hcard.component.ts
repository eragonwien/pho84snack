import { Component, OnInit, Input } from "@angular/core";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-hcard",
  templateUrl: "./hcard.component.html",
  styleUrls: ["./hcard.component.css"]
})
export class HcardComponent implements OnInit {
  @Input() title: string;
  @Input() subtitle: string;
  @Input() text: string;
  @Input() buttonText: string;
  @Input() buttonUrl: string;
  @Input() image: string;
  @Input() index: number;

  constructor(private hs: HelperService) {}

  ngOnInit() {
    this.image = this.hs.imagePath(this.image);
  }

  get backgroundImage(): string {
    return this.hs.imageUrlPath(this.image);
  }

  get alignment(): string {
    return this.index % 2 === 0 ? "left" : "right";
  }
}
