import { Component, OnInit, Input } from "@angular/core";
import { DataService } from "src/app/services/data.service";
import { HelperService } from "src/app/services/helper.service";

@Component({
  selector: "app-gallery",
  templateUrl: "./gallery.component.html",
  styleUrls: ["./gallery.component.css"]
})
export class GalleryComponent implements OnInit {
  @Input() title: string;
  @Input() limit: number;
  gallery: string[];

  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.limit = this.limit ? this.limit : 0;
    this.ds.gallery.subscribe((res: string[]) => {
      this.gallery = res.map(r => this.hs.imagePath(r));
    });
  }

  // check if number of entry excess limit
  offLimit(index: number): boolean {
    return !(this.limit == 0 || index < this.limit);
  }

  background(image: string) {
    return this.hs.imageUrlPath(image);
  }
}
