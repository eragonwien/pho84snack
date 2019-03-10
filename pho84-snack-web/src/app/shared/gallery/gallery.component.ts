import { Component, OnInit, Input } from "@angular/core";
import { DataService } from "src/app/services/data.service";
import { HelperService } from "src/app/services/helper.service";
import { GalleryItem } from "src/app/models";

@Component({
  selector: "app-gallery",
  templateUrl: "./gallery.component.html",
  styleUrls: ["./gallery.component.css"]
})
export class GalleryComponent implements OnInit {
  @Input() title: string;
  @Input() limit: number;
  gallery: GalleryItem[];

  constructor(private ds: DataService, private hs: HelperService) {}

  ngOnInit() {
    this.limit = this.limit ? this.limit : 0;
    this.ds.gallery.subscribe((res: GalleryItem[]) => {
      this.gallery = res;

      this.gallery.forEach(g => {
        g.image = this.hs.imagePath(g.image);
      });
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
