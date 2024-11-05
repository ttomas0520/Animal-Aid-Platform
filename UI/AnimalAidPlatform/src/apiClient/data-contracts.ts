/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface Animal {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  breed: string;
  /** @format byte */
  photo?: string | null;
  /**
   * @format int32
   * @min 0
   * @max 2147483647
   */
  age?: number | null;
  description?: string | null;
  /** @format int32 */
  animalShelterId?: number;
  animalShelter?: AnimalShelter;
}

export interface AnimalShelter {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  type: string;
  description?: string | null;
  /** @format double */
  geoLong: number;
  /** @format double */
  geoLat?: number;
  address?: string | null;
  locUrl?: string | null;
  location?: Point;
  /**
   * @minLength 1
   * @pattern ^[\d\+\-\.\(\)\/\s]*$
   */
  phoneNumber: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  website?: string | null;
  /** @minLength 1 */
  contactName: string;
  /** @minLength 1 */
  contactPosition: string;
  weekdays?: string | null;
  weekend?: string | null;
  adoption?: boolean;
  visiting?: boolean;
  volunteering?: boolean;
  medicalCare?: boolean;
  donations?: boolean;
}

export interface AnimalShelterDTO {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  name: string;
  /** @minLength 1 */
  type: string;
  description?: string | null;
  location?: LocationDTO;
  /**
   * @minLength 1
   * @pattern ^[\d\+\-\.\(\)\/\s]*$
   */
  phoneNumber: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  website?: string | null;
  /** @minLength 1 */
  contactName: string;
  /** @minLength 1 */
  contactPosition: string;
  weekdays?: string | null;
  weekend?: string | null;
  adoption?: boolean;
  visiting?: boolean;
  volunteering?: boolean;
  medicalCare?: boolean;
  donations?: boolean;
}

export interface ApplicationUser {
  id?: string | null;
  userName?: string | null;
  normalizedUserName?: string | null;
  normalizedEmail?: string | null;
  emailConfirmed?: boolean;
  passwordHash?: string | null;
  securityStamp?: string | null;
  concurrencyStamp?: string | null;
  phoneNumberConfirmed?: boolean;
  twoFactorEnabled?: boolean;
  /** @format date-time */
  lockoutEnd?: string | null;
  lockoutEnabled?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
  /** @minLength 1 */
  name: string;
  phoneNumber?: string | null;
  email?: string | null;
  role?: "ADMIN" | "Civilian" | "Activist" | "ShelterWorker" | "ShelterAdmin";
  posts?: FeedPost[] | null;
  notificationSettings?: NotificationSettings;
}

export interface AuthResponseDTO {
  token?: string | null;
  isSucces?: boolean;
  message?: string | null;
}

export interface Category {
  /** @format int32 */
  id?: number;
  name?: string | null;
  urlhandle?: string | null;
  assestIconHref?: string | null;
  posts?: FeedPost[] | null;
  notificationSettings?: NotificationSettings[] | null;
}

export interface CategoryDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
  urlhandle?: string | null;
  assestIconHref?: string | null;
}

export interface CategoryRequestDto {
  name?: string | null;
  urlhandle?: string | null;
  assetsIconHref?: string | null;
}

export interface Coordinate {
  /** @format double */
  x?: number;
  /** @format double */
  y?: number;
  /** @format double */
  z?: number;
  /** @format double */
  m?: number;
  coordinateValue?: Coordinate;
  isValid?: boolean;
}

export type CoordinateEqualityComparer = object;

export interface CoordinateSequence {
  /** @format int32 */
  dimension?: number;
  /** @format int32 */
  measures?: number;
  /** @format int32 */
  spatial?: number;
  ordinates?:
    | "None"
    | "X"
    | "Y"
    | "XY"
    | "Z"
    | "XYZ"
    | "Spatial4"
    | "Spatial5"
    | "Spatial6"
    | "Spatial7"
    | "Spatial8"
    | "Spatial9"
    | "Spatial10"
    | "Spatial11"
    | "Spatial12"
    | "Spatial13"
    | "Spatial14"
    | "Spatial15"
    | "Spatial16"
    | "AllSpatialOrdinates"
    | "M"
    | "XYM"
    | "XYZM"
    | "Measure2"
    | "Measure3"
    | "Measure4"
    | "Measure5"
    | "Measure6"
    | "Measure7"
    | "Measure8"
    | "Measure9"
    | "Measure10"
    | "Measure11"
    | "Measure12"
    | "Measure13"
    | "Measure14"
    | "Measure15"
    | "Measure16"
    | "AllMeasureOrdinates"
    | "AllOrdinates";
  hasZ?: boolean;
  hasM?: boolean;
  /** @format int32 */
  zOrdinateIndex?: number;
  /** @format int32 */
  mOrdinateIndex?: number;
  first?: Coordinate;
  last?: Coordinate;
  /** @format int32 */
  count?: number;
}

export interface CoordinateSequenceFactory {
  ordinates?:
    | "None"
    | "X"
    | "Y"
    | "XY"
    | "Z"
    | "XYZ"
    | "Spatial4"
    | "Spatial5"
    | "Spatial6"
    | "Spatial7"
    | "Spatial8"
    | "Spatial9"
    | "Spatial10"
    | "Spatial11"
    | "Spatial12"
    | "Spatial13"
    | "Spatial14"
    | "Spatial15"
    | "Spatial16"
    | "AllSpatialOrdinates"
    | "M"
    | "XYM"
    | "XYZM"
    | "Measure2"
    | "Measure3"
    | "Measure4"
    | "Measure5"
    | "Measure6"
    | "Measure7"
    | "Measure8"
    | "Measure9"
    | "Measure10"
    | "Measure11"
    | "Measure12"
    | "Measure13"
    | "Measure14"
    | "Measure15"
    | "Measure16"
    | "AllMeasureOrdinates"
    | "AllOrdinates";
}

export interface CreatePostDTO {
  /** @minLength 1 */
  title: string;
  /** @minLength 1 */
  contentText: string;
  location?: LocationDTO;
  shortAddess?: string | null;
  imageUrl?: string | null;
  /** @format int32 */
  categoryId?: number;
}

export interface CreateRoleDTO {
  /** @minLength 1 */
  roleName: string;
}

export interface Envelope {
  isNull?: boolean;
  /** @format double */
  width?: number;
  /** @format double */
  height?: number;
  /** @format double */
  diameter?: number;
  /** @format double */
  minX?: number;
  /** @format double */
  maxX?: number;
  /** @format double */
  minY?: number;
  /** @format double */
  maxY?: number;
  /** @format double */
  area?: number;
  /** @format double */
  minExtent?: number;
  /** @format double */
  maxExtent?: number;
  centre?: Coordinate;
}

export interface FeedPost {
  /** @format int32 */
  id?: number;
  /** @minLength 1 */
  title: string;
  /** @minLength 1 */
  contentText: string;
  /** @format int32 */
  categoryId?: number;
  category?: Category;
  imageUrl?: string | null;
  urlHandle?: string | null;
  /** @format double */
  geoLong?: number;
  /** @format double */
  geoLat?: number;
  address?: string | null;
  locUrl?: string | null;
  location?: Point;
  /** @format date-time */
  postDate?: string;
  creatorId?: string | null;
  creator?: ApplicationUser;
  associatedAnimals?: Animal[] | null;
  postLikes?: FeedPostLike[] | null;
  /** @format int32 */
  likes?: number;
  reports?: Report[] | null;
}

export interface FeedPostLike {
  /** @format int32 */
  feedPostId?: number;
  feedPost?: FeedPost;
  userId?: string | null;
  user?: ApplicationUser;
  /** @format date-time */
  likedAt?: string;
}

export interface FeedPostResponseDTO {
  /** @format int32 */
  id?: number;
  title?: string | null;
  contentText?: string | null;
  location?: LocationDTO;
  category?: CategoryDto;
  userID?: string | null;
  creatorName?: string | null;
  imageUrl?: string | null;
  /** @format int32 */
  likeNumber?: number;
  isLiked?: boolean;
  ownPost?: boolean;
}

export interface Geometry {
  factory?: GeometryFactory;
  userData?: any;
  /** @format int32 */
  srid?: number;
  geometryType?: string | null;
  ogcGeometryType?:
    | "Point"
    | "LineString"
    | "Polygon"
    | "MultiPoint"
    | "MultiLineString"
    | "MultiPolygon"
    | "GeometryCollection"
    | "CircularString"
    | "CompoundCurve"
    | "CurvePolygon"
    | "MultiCurve"
    | "MultiSurface"
    | "Curve"
    | "Surface"
    | "PolyhedralSurface"
    | "TIN";
  precisionModel?: PrecisionModel;
  coordinate?: Coordinate;
  coordinates?: Coordinate[] | null;
  /** @format int32 */
  numPoints?: number;
  /** @format int32 */
  numGeometries?: number;
  isSimple?: boolean;
  isValid?: boolean;
  isEmpty?: boolean;
  /** @format double */
  area?: number;
  /** @format double */
  length?: number;
  centroid?: Point;
  interiorPoint?: Point;
  pointOnSurface?: Point;
  dimension?: "Point" | "Curve" | "Surface" | "Collapse" | "Dontcare" | "True" | "False";
  boundary?: Geometry;
  boundaryDimension?: "Point" | "Curve" | "Surface" | "Collapse" | "Dontcare" | "True" | "False";
  envelope?: Geometry;
  envelopeInternal?: Envelope;
  isRectangle?: boolean;
}

export interface GeometryFactory {
  precisionModel?: PrecisionModel;
  coordinateSequenceFactory?: CoordinateSequenceFactory;
  /** @format int32 */
  srid?: number;
  geometryServices?: NtsGeometryServices;
}

export type GeometryOverlay = object;

export interface LocationDTO {
  /** @format double */
  latitude?: number;
  /** @format double */
  longitude?: number;
  address?: string | null;
  url?: string | null;
}

export interface LoginDTO {
  /** @minLength 1 */
  email: string;
  /** @minLength 1 */
  password: string;
}

export interface NotificationSettings {
  /** @format int32 */
  id?: number;
  userId?: string | null;
  user?: ApplicationUser;
  pushNotificationEnabled?: boolean;
  /** @format double */
  geoLong?: number;
  /** @format double */
  geoLat?: number;
  address?: string | null;
  location?: Point;
  /** @format double */
  radius?: number;
  categories?: Category[] | null;
}

export interface NotificationSettingsDto {
  pushNotificationEnabled?: boolean;
  location?: LocationDTO;
  /** @format double */
  radius?: number;
  categoryIds?: number[] | null;
}

export interface NtsGeometryServices {
  geometryOverlay?: GeometryOverlay;
  coordinateEqualityComparer?: CoordinateEqualityComparer;
  /** @format int32 */
  defaultSRID?: number;
  defaultCoordinateSequenceFactory?: CoordinateSequenceFactory;
  defaultPrecisionModel?: PrecisionModel;
}

export interface Point {
  factory?: GeometryFactory;
  userData?: any;
  /** @format int32 */
  srid?: number;
  precisionModel?: PrecisionModel;
  /** @format int32 */
  numGeometries?: number;
  isSimple?: boolean;
  isValid?: boolean;
  /** @format double */
  area?: number;
  /** @format double */
  length?: number;
  centroid?: Point;
  interiorPoint?: Point;
  pointOnSurface?: Point;
  envelope?: Geometry;
  envelopeInternal?: Envelope;
  isRectangle?: boolean;
  coordinateSequence?: CoordinateSequence;
  coordinates?: Coordinate[] | null;
  /** @format int32 */
  numPoints?: number;
  isEmpty?: boolean;
  dimension?: "Point" | "Curve" | "Surface" | "Collapse" | "Dontcare" | "True" | "False";
  boundaryDimension?: "Point" | "Curve" | "Surface" | "Collapse" | "Dontcare" | "True" | "False";
  /** @format double */
  x?: number;
  /** @format double */
  y?: number;
  coordinate?: Coordinate;
  geometryType?: string | null;
  ogcGeometryType?:
    | "Point"
    | "LineString"
    | "Polygon"
    | "MultiPoint"
    | "MultiLineString"
    | "MultiPolygon"
    | "GeometryCollection"
    | "CircularString"
    | "CompoundCurve"
    | "CurvePolygon"
    | "MultiCurve"
    | "MultiSurface"
    | "Curve"
    | "Surface"
    | "PolyhedralSurface"
    | "TIN";
  boundary?: Geometry;
  /** @format double */
  z?: number;
  /** @format double */
  m?: number;
}

export interface PrecisionModel {
  isFloating?: boolean;
  /** @format int32 */
  maximumSignificantDigits?: number;
  /** @format double */
  scale?: number;
  /** @format double */
  gridSize?: number;
  precisionModelType?: "Floating" | "FloatingSingle" | "Fixed";
}

export interface RegisterDTO {
  /** @minLength 1 */
  name: string;
  /**
   * @format email
   * @minLength 1
   */
  email: string;
  /** @minLength 1 */
  password: string;
  /** @minLength 1 */
  phoneNumber: string;
  roles?: string[] | null;
}

export interface Report {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  feedPostId?: number;
  feedPost?: FeedPost;
  /** @minLength 1 */
  reporterId: string;
  reporter?: ApplicationUser;
  /** @minLength 1 */
  reason: string;
  /** @format date-time */
  reportDate?: string;
  isResolved?: boolean;
  adminResponse?: string | null;
}

export interface ReportDTO {
  /** @format int32 */
  feedPostId: number;
  /**
   * @minLength 0
   * @maxLength 500
   */
  reason: string;
}

export interface ResolveReportDTO {
  /**
   * @minLength 0
   * @maxLength 500
   */
  adminResponse: string;
  isResolved: boolean;
  /** @default "None" */
  reportAction: "None" | "Delete" | "NoticeCreator";
}

export interface RoleDTO {
  id?: string | null;
  roleName?: string | null;
  /** @format int32 */
  userCount?: number;
}

export interface UserDetailDTO {
  id?: string | null;
  name?: string | null;
  email?: string | null;
  roles?: string[] | null;
  phoneNumber?: string | null;
  twoFactorEnabled?: boolean;
  phoneNumberConfirmed?: boolean;
  /** @format int32 */
  accessFailedCount?: number;
}
