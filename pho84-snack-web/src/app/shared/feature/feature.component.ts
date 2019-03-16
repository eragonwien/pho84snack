import { Component, OnInit, Input } from "@angular/core";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-feature",
  templateUrl: "./feature.component.html",
  styleUrls: ["./feature.component.css"]
})
export class FeatureComponent implements OnInit {
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
    this.buttonUrl = this.hs.getPath(this.buttonUrl);
  }

  get backgroundImage(): string {
    return this.hs.imageUrlPath(this.image);
  }

  get even(): boolean {
    return this.index % 2 === 0;
  }
}
