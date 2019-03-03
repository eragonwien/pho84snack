import { Component, OnInit } from "@angular/core";
import { DataService } from "../services/data.service";
import { Feature } from "../models";
import { HelperService } from "../services/helper.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {
  features: Feature[];
  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.ds.features.subscribe((res: Feature[]) => {
      this.features = res;
    });
  }
}
