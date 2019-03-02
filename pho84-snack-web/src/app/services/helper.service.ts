import { Injectable } from "@angular/core";

@Injectable()
export class HelperService {
  DEFAULT_IMAGE: string = "default";

  constructor() {}

  imagePath(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "/assets/images/compressed/" + image + ".jpg";
  }

  imageUrlPath(image: string): string {
    if (!image) {
      image = this.DEFAULT_IMAGE;
    }
    return "url('" + image + "')";
  }
}
