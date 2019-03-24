import { Component, OnInit, Input, HostListener, ElementRef, ViewChild } from "@angular/core";
import { HelperService } from "src/app/services/helper.service";
import { Feature } from "src/app/models";
import { slideInLeft, slideInRight } from "src/app/shared/animations";

@Component({
  selector: "app-feature",
  templateUrl: "./feature.component.html",
  styleUrls: ["./feature.component.css"],
  animations: [slideInLeft, slideInRight]
})
export class FeatureComponent implements OnInit {
  @Input() index: number;
  @Input() feature: Feature;

  @ViewChild("mainScreen") elementView: ElementRef;

  scrollState: string = "hide";

  constructor(private hs: HelperService, private el: ElementRef) {}

  ngOnInit() {
    this.feature.image = this.hs.image(this.feature.image);
    this.feature.url = this.hs.getPath(this.feature.url);
  }

  get backgroundImage(): string {
    return this.hs.imageUrlPath(this.feature.image);
  }

  get even(): boolean {
    return this.index % 2 === 0;
  }

  @HostListener("window:scroll", ["$event"])
  checkScroll() {
    this.scrollState = this.hs.getScrollState(this.scrollState, this.el);
  }
}
