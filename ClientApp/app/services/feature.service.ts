import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class FeatureService {

  constructor(private http: Http) { }

  getFeatures() {
    return this.http.get('/api/features')
      .map(res => res.json());
  }
}
