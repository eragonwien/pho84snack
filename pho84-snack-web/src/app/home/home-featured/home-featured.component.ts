import { Component, OnInit, Input } from "@angular/core";
import { Feature } from "src/app/models";
import { DataService } from "src/app/services/data.service";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-home-featured",
  templateUrl: "./home-featured.component.html",
  styleUrls: ["./home-featured.component.css"]
})
export class HomeFeaturedComponent implements OnInit {
  features: Feature[];

  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.ds.features.subscribe((res: Feature[]) => {
      this.features = res;
    });
  }

  backgroundImg(feature: Feature) {
    return this.hs.imageUrlPath(feature.image);
  }

  img(feature: Feature) {
    return this.hs.imagePath(feature.image);
  }
}
