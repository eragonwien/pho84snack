import { Component, OnInit, Input } from "@angular/core";
import { Product } from "src/app/models";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-price-list",
  templateUrl: "./price-list.component.html",
  styleUrls: ["./price-list.component.css"]
})
export class PriceListComponent implements OnInit {
  @Input() title: string;
  @Input() subtitle: string;
  @Input() image: string;
  @Input() products: Product[];
  @Input() index: number;

  constructor(private hs: HelperService) {}

  ngOnInit() {}

  get backgroundImage() {
    return this.hs.imageUrlPath(this.hs.imagePath(this.image));
  }

  get hasTextSuccess(): boolean {
    return this.index % 2 !== 0;
  }
}
