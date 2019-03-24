import { Component, OnInit, Input, HostListener, ElementRef } from "@angular/core";
import { Product } from "src/app/models";
import { HelperService } from "src/app/services/helper.service";
import { slideInRight } from "../animations";

@Component({
  selector: "app-price-list",
  templateUrl: "./price-list.component.html",
  styleUrls: ["./price-list.component.css"],
  animations: [slideInRight]
})
export class PriceListComponent implements OnInit {
  @Input() title: string;
  @Input() subtitle: string;
  @Input() image: string;
  @Input() products: Product[];
  @Input() index: number;

  scrollState: string = "hide";

  constructor(private hs: HelperService, private el: ElementRef) {}

  ngOnInit() {}

  get hasTextSuccess(): boolean {
    return this.index % 2 !== 0;
  }

  @HostListener("window:scroll", ["$event"])
  checkScroll() {
    this.scrollState = this.hs.getScrollState(this.scrollState, this.el);
  }
}
